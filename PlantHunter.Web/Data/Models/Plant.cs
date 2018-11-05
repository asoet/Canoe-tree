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

        public Plant(double longitude, double latitude, string name, string deviceId, string scientificName, string family, string endangeredLevel, string surrounding, string description)
        {
            Longitude = longitude;
            Latitude = latitude;
            Name = name;
            DeviceId = deviceId;
            ScientificName = scientificName;
            Family = family;
            EndangeredLevel = endangeredLevel;
            Surrounding = surrounding;
            Description = description;
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
        [DisplayName("Scientific name")]
        public string ScientificName { get; set; }
        public string Family { get; set; }
        [DisplayName("Endangered level")]
        public string EndangeredLevel { get; set; }
        public string Surrounding { get; set; }
        public string Description { get; set; }
    }
}
