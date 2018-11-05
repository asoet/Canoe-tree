using System;
using System.Collections.Generic;
using System.Text;

namespace PlantHunter.Mobile.Core.Models
{
    public class Plant
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public Guid Id { get; set; }
        public string PlantFileUrl { get; set; }
        public string Name { get; set; }
        public string DeviceId { get; set; }
        public long Points { get; set; }
    }
}
