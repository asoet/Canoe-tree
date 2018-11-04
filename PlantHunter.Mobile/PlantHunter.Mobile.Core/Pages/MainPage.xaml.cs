// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using MvvmCross;
using MvvmCross.Forms.Views;
using PlantHunter.Mobile.Core.Services;
using PlantHunter.Mobile.Core.ViewModels;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace PlantHunter.Mobile.Core.Pages
{
    public partial class MainPage : MvxContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            map.InitialCameraUpdate = CameraUpdateFactory.NewPositionZoom(new Position(20.929120d, -157.582678d), 5d);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () =>
            {
                var allPlants = await Mvx.Resolve<IApiService>().GetAllPlantsAsync();
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
                    pin.c
                    map.Pins.Add(pin);
                }
            });
           
        }
    }
}
