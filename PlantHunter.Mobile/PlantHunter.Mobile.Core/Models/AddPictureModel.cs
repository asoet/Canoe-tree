using System;
using System.Collections.Generic;
using System.Text;

namespace PlantHunter.Mobile.Core.Models
{
    public class AddPictureModel
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Name { get; set; }
        public string DeviceId { get; set; }
    }
}
