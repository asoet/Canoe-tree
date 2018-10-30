// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PlantHunter.Mobile.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly Services.IAppSettings _settings;
        private readonly IUserDialogs _userDialogs;

        public MainViewModel(IMvxNavigationService navigationService, Services.IAppSettings settings,
            IUserDialogs userDialogs)
        {
            _navigationService = navigationService;
            _settings = settings;
            _userDialogs = userDialogs;
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
            });
    }
}