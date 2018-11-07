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
        public string ScientificName { get; set; }
        public string Family { get; set; }
        public string EndangeredLevel { get; set; }
        public string Surrounding { get;  set; }
        public string Description { get; set; }
        public string PushRegistrationToken { get; set; }
    }
}
