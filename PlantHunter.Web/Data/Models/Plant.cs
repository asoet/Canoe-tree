using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantHunter.Mobile.Web.Data.Models
{
    public class Plant
    {
        
        public Plant(double longitude, double latitude)
        {
            Id = Guid.NewGuid();
            this.Longitude = longitude;
            this.Latitude = latitude;
        }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public Guid Id { get; set; }
        public string PlantFileUrl { get; set; }
    }
}
