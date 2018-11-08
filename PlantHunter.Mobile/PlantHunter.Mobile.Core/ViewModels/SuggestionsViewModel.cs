using MvvmHelpers;
using Plugin.DeviceInfo;
using Plugin.Media;
using PlantHunter.Mobile.Core.Exceptions;
using PlantHunter.Mobile.Core.Models;
using PlantHunter.Mobile.Core.Services.Geolocator;
using PlantHunter.Mobile.Core.Services.Plants;
using PlantHunter.Mobile.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PlantHunter.Mobile.Core.ViewModels
{
    public class SuggestionsViewModel : ViewModelBase
    {
        ObservableRangeCollection<CustomPin> customPins;
        
        readonly IPlantService plantService;
        readonly ILocationService locationService;

        public SuggestionsViewModel(
            ILocationService locationService,
            IPlantService plantService)
        {
            this.locationService = locationService;
            this.plantService = plantService;
        }




        public ICommand TakePictureCommand => new AsyncCommand(TakePicture);

        async Task TakePicture()
        {
            await CrossMedia.Current.Initialize();
            try
            {
                IsBusy = true;
                var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { CompressionQuality = 50 });
                IsBusy = true;
                if (photo == null)
                    await DialogService.ShowAlertAsync("No picture selected", "Picture", "Ok");
                else
                {
                    var navigationParameter = new Dictionary<string, object>
                {
                    { "photo", photo },
                };

                    await NavigationService.NavigateToAsync<PlantDetailsViewModel>(navigationParameter);
                    IsBusy = false;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
          
        }


        public override async Task InitializeAsync(object navigationData)
        {
            
        }

      
    }
}