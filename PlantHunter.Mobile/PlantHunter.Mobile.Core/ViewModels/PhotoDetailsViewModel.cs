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
using System.Collections.Generic;
using Xamarin.Forms;

namespace PlantHunter.Mobile.Core.ViewModels
{
    public class PhotoDetailsViewModel : MvxViewModel<ImageSource>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly Services.IAppSettings _settings;
        private readonly IUserDialogs _userDialogs;

        public ImageSource PictureSource { get; set; }
        public string PlantName { get; set; }
        private readonly ILocalizeService _localizeService;

        public PhotoDetailsViewModel(IMvxNavigationService navigationService, Services.IAppSettings settings, IUserDialogs userDialogs, ILocalizeService localizeService)
        {
            _navigationService = navigationService;
            _settings = settings;
            _userDialogs = userDialogs;
            _localizeService = localizeService;
            
        }

        public override void Prepare(ImageSource parameter)
        {
            PictureSource = parameter;
        }
    }
}