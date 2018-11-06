// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.IoC;
using MvvmCross.Plugin.Json;
using MvvmCross.ViewModels;
using PlantHunter.Mobile.Core.Services;
using Plugin.AzurePushNotification;
using System.Net.Http;
using Xamarin.Forms;

namespace PlantHunter.Mobile.Core
{
    public class MvxApp : MvxApplication
    {

        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes().
                EndingWith("Repository")
                .AsTypes()
                .RegisterAsLazySingleton();

            Mvx.RegisterType<Services.IAppSettings, Services.AppSettings>();
            Mvx.RegisterType<IMvxJsonConverter, MvxJsonConverter>();
            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            Mvx.RegisterSingleton<HttpClient>(() => new HttpClient() );

            Resources.AppResources.Culture = Mvx.Resolve<Services.ILocalizeService>().GetCurrentCultureInfo();
            

            RegisterAppStart<ViewModels.MainViewModel>();

            var appSettings = Mvx.Resolve<IAppSettings>();
            if (string.IsNullOrEmpty(appSettings.PushRegistrationId))
            {
                CrossAzurePushNotification.Current.RegisterForPushNotifications();
            }
            CrossAzurePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                appSettings.PushRegistrationId = p.Token;
            };
        }
    }
}
