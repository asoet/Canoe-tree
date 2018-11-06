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
            Mvx.RegisterType<IPushRegistration,PushRegistration>();
            Mvx.RegisterType<IMvxJsonConverter, MvxJsonConverter>();
            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            Mvx.RegisterSingleton<HttpClient>(() => new HttpClient() );

            Resources.AppResources.Culture = Mvx.Resolve<Services.ILocalizeService>().GetCurrentCultureInfo();
            
            RegisterAppStart<ViewModels.MainViewModel>();

            if(Mvx.TryResolve(out ITokenReceiver tokenReceiver))
            {
                var token = tokenReceiver.GetHandle();
                if(!string.IsNullOrEmpty(token))
                {
                    Mvx.Resolve<IAppSettings>().Handle = token;
                    Mvx.Resolve<IPushRegistration>().Init();
                }
               
            }
        }

    }
}
