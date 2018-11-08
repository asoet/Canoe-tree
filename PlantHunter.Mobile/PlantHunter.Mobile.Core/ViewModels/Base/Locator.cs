using System;
using Autofac;
using PlantHunter.Mobile.Core.Services.Analytic;
using PlantHunter.Mobile.Core.Services.Chart;
using PlantHunter.Mobile.Core.Services.Dialog;
using PlantHunter.Mobile.Core.Services.OpenUri;
using PlantHunter.Mobile.Core.Services.Request;
using PlantHunter.Mobile.Core.Services.Settings;
using PlantHunter.Mobile.Core.Models;
using PlantHunter.Mobile.Core.Services.File;
using PlantHunter.Mobile.Core.Services.Geolocator;
using PlantHunter.Mobile.Core.Services.Plants;
using PlantHunter.Mobile.Core.Services.Push;
using PlantHunter.Mobile.Core.Services.Navigation;

namespace PlantHunter.Mobile.Core.ViewModels.Base
{
    public class Locator
    {
        IContainer container;
        ContainerBuilder containerBuilder;

        public static Locator Instance { get; } = new Locator();

        public Locator()
        {
            containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<AnalyticService>().As<IAnalyticService>();
            containerBuilder.RegisterType<DialogService>().As<IDialogService>();
            containerBuilder.RegisterType<NavigationService>().As<INavigationService>();
            containerBuilder.RegisterType<FakeChartService>().As<IChartService>();
            containerBuilder.RegisterType<LocationService>().As<ILocationService>();
            containerBuilder.RegisterType<OpenUriService>().As<IOpenUriService>();
            containerBuilder.RegisterType<RequestService>().As<IRequestService>();
            containerBuilder.RegisterType<FileService>().As<IFileService>();
            containerBuilder.RegisterType(typeof(SettingsService)).As(typeof(ISettingsService<RemoteSettings>));

            containerBuilder.RegisterType<PlantService>().As<IPlantService>();
            containerBuilder.RegisterType<TokenReceiver>().As<ITokenReceiver>();
            
            containerBuilder.RegisterType<PlantDetailsViewModel>();
            containerBuilder.RegisterType<MainViewModel>();
            containerBuilder.RegisterType<SuggestionsViewModel>();
            containerBuilder.RegisterType<ExtendedSplashViewModel>();
        }

        public T Resolve<T>() => container.Resolve<T>();

        public object Resolve(Type type) => container.Resolve(type);

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface => containerBuilder.RegisterType<TImplementation>().As<TInterface>();

        public void Register<T>() where T : class => containerBuilder.RegisterType<T>();

        public void Build() => container = containerBuilder.Build();
    }
}