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
    }
}
