using System.Threading.Tasks;
using PlantHunter.Mobile.Core.ViewModels.Base;

namespace PlantHunter.Mobile.Core.ViewModels
{
    public class ExtendedSplashViewModel : ViewModelBase
    {
        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            await NavigationService.InitializeAsync();

            IsBusy = false;
        }
    }
}
