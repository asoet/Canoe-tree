using Android.App;
using Android.Views.InputMethods;
using PlantHunter.Mobile.Core.Services.DismissKeyboard;
using PlantHunter.Mobile.Droid.Services.DismissKeyboard;

[assembly: Xamarin.Forms.Dependency(typeof(DismissKeyboardService))]
namespace PlantHunter.Mobile.Droid.Services.DismissKeyboard
{
    public class DismissKeyboardService : IDismissKeyboardService
    {
        public void DismissKeyboard()
        {
            var inputMethodManager = InputMethodManager.FromContext(Android.App.Application.Context);

            inputMethodManager.HideSoftInputFromWindow(
                ((Activity)Xamarin.Forms.Forms.Context).Window.DecorView.WindowToken, HideSoftInputFlags.NotAlways);
        }
    }
}