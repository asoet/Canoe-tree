using System.Collections.Generic;

namespace PlantHunter.Mobile.Core.Services.Analytic
{
    public class DummyAnalyticService : IAnalyticService
    {
        public void TrackEvent(string name, Dictionary<string, string> properties = null)
        {
            // Do nothing
        }
    }
}
