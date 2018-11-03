// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PlantHunter.Mobile.Core.Services;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace PlantHunter.Mobile.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAppSettings _settings;
        private readonly IUserDialogs _userDialogs;
        private readonly IApiService _apiService;

        public Map Map { get; set; }
        public bool TakePictureButtonEnabled { get; set; } = true;

        public MainViewModel(IMvxNavigationService navigationService, Services.IAppSettings settings,
            IUserDialogs userDialogs,
            IApiService apiService)
        {
            _navigationService = navigationService;
            _settings = settings;
            _userDialogs = userDialogs;
            _apiService = apiService;

            
           
        }

        public override async void ViewAppearing()
        {
            var allPlants = (await _apiService.GetAllPlantsAsync());
            var plantsSubSet = allPlants.Where(f => f.Longitude != default && f.Latitude != default && f.Name != default);
            foreach (var item in plantsSubSet)
            {
                var pin = new Pin()
                {
                    Type = PinType.SavedPin,
                    Label = item.Name,
                    Position = new Position(item.Latitude, item.Longitude),
                    Tag = item
                };
                Map.Pins.Add(pin);
            }
        }

        public IMvxAsyncCommand TakePictureCommand =>
            new MvxAsyncCommand(async () =>
            {
                var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { CompressionQuality = 50 });
                if (photo == null)
                    _userDialogs.Alert("No picture selected");
                else
                {
                    var imageSource = ImageSource.FromStream(() => photo.GetStream());
                    await _navigationService.Navigate<PhotoDetailsViewModel, (ImageSource source, MediaFile photo)>((imageSource, photo));
                }
                TakePictureButtonEnabled = true;
            });
    }
}