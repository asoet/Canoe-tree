// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using MvvmCross.Forms.Views;
using PlantHunter.Mobile.Core.ViewModels;
using Xamarin.Forms.GoogleMaps;

namespace PlantHunter.Mobile.Core.Pages
{
    public partial class MainPage : MvxContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            map.InitialCameraUpdate = CameraUpdateFactory.NewPositionZoom(new Position(20.929120d, -157.582678d), 40d);
        }
      
    }
}
