using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PlantHunter.Mobile.Web.Data.Models
{
    public class Plant
    {
        public Plant()
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

        public void EditPoints(long points)
        {
            Points = points;
        }

        [DisplayName("Location (long)")]
        public double Longitude { get; set; }
        [DisplayName("Location (long)")]
        public double Latitude { get; set; }
        public Guid Id { get; set; }
        [DisplayName("Image")]
        public string PlantFileUrl { get; set; }
        public string Name { get; set; }
        [DisplayName("User")]
        public string DeviceId { get; set; }
        public long Points { get; set; }
    }
}
