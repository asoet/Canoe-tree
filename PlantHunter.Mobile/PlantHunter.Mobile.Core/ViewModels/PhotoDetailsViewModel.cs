// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PlantHunter.Mobile.Core.Helpers;
using PlantHunter.Mobile.Core.Services;
using Plugin.Media.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Xamarin.Forms;

namespace PlantHunter.Mobile.Core.ViewModels
{
    public class PhotoDetailsViewModel : MvxViewModel<(ImageSource source, MediaFile photo)>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly Services.IAppSettings _settings;
        private readonly IUserDialogs _userDialogs;
        private readonly ILocalizeService _localizeService;
        private readonly IApiService _apiService;

        public ImageSource PictureSource { get; set; }
        public MediaFile Photo { get; set; }
        public string PlantName { get; set; }

        public PhotoDetailsViewModel(IMvxNavigationService navigationService, IAppSettings settings, IUserDialogs userDialogs, ILocalizeService localizeService, IApiService apiService)
        {
            _navigationService = navigationService;
            _settings = settings;
            _userDialogs = userDialogs;
            _localizeService = localizeService;
            _apiService = apiService;
        }


        public override void Prepare((ImageSource source, MediaFile photo) parameter)
        {
            PictureSource = parameter.source;
            Photo = parameter.photo;
        }


        public IMvxAsyncCommand UploadPictureCommand =>
            new MvxAsyncCommand(async () =>
            {
                if(await _apiService.UploadPictureAsync(Photo.GetStream(), Path.GetExtension(Photo.Path)))
                {
                    _userDialogs.Alert("Uploaded successfully");
                }
                else
                {
                    _userDialogs.Alert("Upload failed");
                }
            });
    }
}