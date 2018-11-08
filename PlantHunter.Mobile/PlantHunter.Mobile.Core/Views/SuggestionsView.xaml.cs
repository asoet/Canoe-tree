using MvvmHelpers;
using Plugin.DeviceInfo;
using PlantHunter.Mobile.Core.Exceptions;
using PlantHunter.Mobile.Core.Helpers;
using PlantHunter.Mobile.Core.Models;
using PlantHunter.Mobile.Core.Services.Dialog;
using PlantHunter.Mobile.Core.Services.Geolocator;
using PlantHunter.Mobile.Core.Services.Plants;
using PlantHunter.Mobile.Core.ViewModels;
using PlantHunter.Mobile.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Xamarin.Forms;
using System.ComponentModel;

namespace PlantHunter.Mobile.Core.Views
{
    public partial class SuggestionsView : ContentPage
    {
        public ObservableRangeCollection<Plant> Plants { get; set; }

        
        public ObservableRangeCollection<CustomPin> CustomPins { get; set; } = new ObservableRangeCollection<CustomPin>();

        public SuggestionsView()
        {
            if (Device.RuntimePlatform != Device.iOS)
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }

            NavigationPage.SetBackButtonTitle(this, string.Empty);

            InitializeComponent();

            MapHelper.CenterMapInDefaultLocation(this.Map);
            Map.IsShowingUser = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            StatusBarHelper.Instance.MakeTranslucentStatusBar(false);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    busyIndicator.IsVisible = true;
                    busyIndicator.IsRunning = true;
                    try
                    {
                       
                        var location = await Locator.Instance.Resolve<ILocationService>().GetPositionAsync();
                        Plants = await Locator.Instance.Resolve<IPlantService>().GetPlantsAsync();
                        plantList.ItemsSource = Plants;
                        CustomPins.Clear();
                        foreach (var plant in Plants)
                        {
                            plant.PlantType = plant.DeviceId == CrossDeviceInfo.Current.Id ? PlantType.own : PlantType.other;
                            CustomPins.Add(new CustomPin
                            {
                                Label = plant.Name,
                                Position = new Xamarin.Forms.Maps.Position(plant.Latitude, plant.Longitude),
                                Type = plant.DeviceId == CrossDeviceInfo.Current.Id ? PlantType.own : PlantType.other
                            });
                        }
                        Map.CustomPins = CustomPins;
                        
                        
                    }
                    catch (HttpRequestException httpEx)
                    {
                        Debug.WriteLine($"[Suggestions] Error retrieving data: {httpEx}");

                        if (!string.IsNullOrEmpty(httpEx.Message))
                        {
                            await Locator.Instance.Resolve<IDialogService>().ShowAlertAsync(
                                string.Format("Exception", httpEx.Message),
                                "Exception",
                                "Ok");
                        }
                    }
                    catch (ConnectivityException cex)
                    {
                        Debug.WriteLine($"[Suggestions] Connectivity Error: {cex}");
                        await Locator.Instance.Resolve<IDialogService>().ShowAlertAsync("There is no Internet conection, try again later.", "Error", "Ok");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"[Suggestions] Error: {ex}");

                        await Locator.Instance.Resolve<IDialogService>().ShowAlertAsync(
                            "Exception",
                            "Exception",
                            "Ok");
                    }
                    finally
                    {
                        IsBusy = false;
                        busyIndicator.IsVisible = false;
                        busyIndicator.IsRunning = false;
                    }

                });

        }
    }
}