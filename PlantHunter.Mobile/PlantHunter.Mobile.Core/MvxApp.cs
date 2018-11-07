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
using PlantHunter.Mobile.Core.Models;
using PlantHunter.Mobile.Core.Services;
using Plugin.AzurePushNotification;
using Plugin.DeviceInfo;
using System.Net.Http;
using System.Threading.Tasks;
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
            Mvx.RegisterType<IPushRegistration, PushRegistration>();
            Mvx.RegisterType<IMvxJsonConverter, MvxJsonConverter>();
            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            Mvx.RegisterSingleton<HttpClient>(() => new HttpClient());

            Resources.AppResources.Culture = Mvx.Resolve<Services.ILocalizeService>().GetCurrentCultureInfo();

            RegisterAppStart<ViewModels.MainViewModel>();

            var appSettings = Mvx.Resolve<IAppSettings>();
            var apiService = Mvx.Resolve<IApiService>();
            if(Mvx.TryResolve<ITokenReceiver>(out ITokenReceiver tokenReceiver))
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await InitPush(tokenReceiver.GetHandle());
                });
            }
        }

        public async Task InitPush(string token)
        {
            var appSettings = Mvx.Resolve<IAppSettings>();
            appSettings.Handle = token;
            var apiService = Mvx.Resolve<IApiService>();
            if (string.IsNullOrEmpty(appSettings.PushRegistrationId))
            {
              
                //var registrationId = await apiService.GetPushRegistrationId();
                var handle = token;
                MobilePlatform platform = MobilePlatform.gcm;
                if(Device.RuntimePlatform == Device.Android)
                {
                    platform = MobilePlatform.gcm;
                }
                if (Device.RuntimePlatform == Device.iOS)
                {
                    platform = MobilePlatform.apns;
                }


                var deviceUpdate = new DeviceRegistration()
                {
                    Handle = handle,
                    Platform = platform,
                    Tags = new string[1] { token },
                    DeviceId = CrossDeviceInfo.Current.Id
                };

                var result = await apiService.EnablePushNotifications(handle, deviceUpdate);
            }
        }
    }
}
