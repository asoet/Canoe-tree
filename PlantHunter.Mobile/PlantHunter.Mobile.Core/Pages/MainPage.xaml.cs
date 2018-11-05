// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using MvvmCross;
using MvvmCross.Forms.Views;
using MvvmCross.Navigation;
using PlantHunter.Mobile.Core.Models;
using PlantHunter.Mobile.Core.Services;
using PlantHunter.Mobile.Core.ViewModels;
using Plugin.DeviceInfo;
using Plugin.Media.Abstractions;
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
            totalPoints.Text = "- points";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () =>
            {
                var allPlants = await Mvx.Resolve<IApiService>().GetAllPlantsAsync();
                var plantsSubSet = allPlants.Where(f => f.Longitude != default && f.Latitude != default && f.Name != default);
                totalPoints.Text = plantsSubSet.Where(f=>f.DeviceId == CrossDeviceInfo.Current.Id).Sum(f => f.Points) + " points";
                foreach (var item in plantsSubSet)
                {
                    Color color = item.DeviceId == CrossDeviceInfo.Current.Id ? Color.Red : Color.Blue;
                    var pin = new Pin()
                    {
                        Type = PinType.SavedPin,
                        Label = item.Name,
                        Position = new Position(item.Latitude, item.Longitude),
                        Tag = item,
                        Icon = BitmapDescriptorFactory.DefaultMarker(color)
                };
                    map.Pins.Add(pin);
                }

            });
            map.InfoWindowClicked += InfoWindowClicked;
        }

        private void InfoWindowClicked(object sender, InfoWindowClickedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Mvx.Resolve<IMvxNavigationService>().Navigate<PhotoDetailsViewModel, (ImageSource source, MediaFile photo, Plant plant)>((default, default, e.Pin.Tag as Plant));
            });
        }
    }
}
