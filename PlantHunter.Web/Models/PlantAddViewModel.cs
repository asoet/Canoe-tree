using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantHunter.Mobile.Web.Models
{
    public class PlantAddViewModel
    {
        public string ImageBase64 { get; set; }
        public string ContentType { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Name { get; set; }
        public string DeviceId { get; set; }
        public string ScientificName { get; set; }
        public string Family { get; set; }
        public string EndangeredLevel { get; set; }
        public string Surrounding { get; set; }
        public string Description { get; set; }
    }
}
