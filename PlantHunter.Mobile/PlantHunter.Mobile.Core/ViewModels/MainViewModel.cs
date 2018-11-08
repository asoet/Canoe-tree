using PlantHunter.Mobile.Core.ViewModels.Base;
using System.Threading.Tasks;

namespace PlantHunter.Mobile.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel()
        {
        }


        public override Task InitializeAsync(object navigationData) => Task.WhenAll
                (
                );
    }
}