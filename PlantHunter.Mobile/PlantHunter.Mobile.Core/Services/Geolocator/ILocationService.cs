using PlantHunter.Mobile.Core.Models;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PlantHunter.Mobile.Core.Services.Geolocator
{
    public interface ILocationService
    {
        Task<Location> GetPositionAsync();
    }
}