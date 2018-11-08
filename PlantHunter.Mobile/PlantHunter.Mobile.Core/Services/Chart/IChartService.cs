using System.Threading.Tasks;

namespace PlantHunter.Mobile.Core.Services.Chart
{
    public interface IChartService
    {
        Task<Microcharts.Chart> GetTemperatureChartAsync();
        Task<Microcharts.Chart> GetGreenChartAsync();
    }
}