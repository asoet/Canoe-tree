using System.Collections.Generic;

namespace PlantHunter.Mobile.Core.Services.Analytic
{
    public interface IAnalyticService
    {
        void TrackEvent(string name, Dictionary<string, string> properties = null);
    }
}