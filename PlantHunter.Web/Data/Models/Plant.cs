using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantHunter.Mobile.Web.Data.Models
{
    public class Plant
    {
        private Plant()
        {

        }
        public Plant(double longitude, double latitude, string name, string deviceId)
        {
            Id = Guid.NewGuid();
            Longitude = longitude;
            Latitude = latitude;
            Name = name;
            DeviceId = deviceId;
        }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public Guid Id { get; set; }
        public string PlantFileUrl { get; set; }
        public string Name { get; set; }
        public string DeviceId { get; set; }
    }
}
