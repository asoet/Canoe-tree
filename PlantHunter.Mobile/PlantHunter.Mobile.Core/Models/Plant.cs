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
        public string ScientificName { get; set; }
        public string Family { get; set; }
        public string EndangeredLevel { get; set; }
        public string Surrounding { get; set; }
        public string Description { get; set; }
        public PlantType PlantType { get; set; }
    }
}
