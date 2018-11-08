using PlantHunter.Mobile.Core.Services.DismissKeyboard;
using PlantHunter.Mobile.UWP.Services.DismissKeyboard;
using Windows.UI.ViewManagement;

[assembly: Xamarin.Forms.Dependency(typeof(DismissKeyboardService))]
namespace PlantHunter.Mobile.UWP.Services.DismissKeyboard
{
    class DismissKeyboardService : IDismissKeyboardService
    {
        public void DismissKeyboard() => InputPane.GetForCurrentView().TryHide();
    }
}