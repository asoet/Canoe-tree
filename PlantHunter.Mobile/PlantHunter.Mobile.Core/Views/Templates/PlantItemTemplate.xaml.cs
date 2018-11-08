using PlantHunter.Mobile.Core.Models;
using PlantHunter.Mobile.Core.Services.Navigation;
using PlantHunter.Mobile.Core.ViewModels;
using PlantHunter.Mobile.Core.ViewModels.Base;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace PlantHunter.Mobile.Core.Views.Templates
{
    public partial class PlantItemTemplate : ContentView
    {
        
        async void PinOrItemCLickedAsync()
        {
            var plant = this.BindingContext as Plant;
            if (plant != null)
            {
                var navigationParameter = new Dictionary<string, object>
                {
                    { "plant", plant },
                };

                await Locator.Instance.Resolve<INavigationService>().NavigateToAsync<PlantDetailsViewModel>(navigationParameter);
            }
        }

        public PlantItemTemplate()
        {
            InitializeComponent();
            tapped.Command = new Command(PinOrItemCLickedAsync);
        }
    }
}