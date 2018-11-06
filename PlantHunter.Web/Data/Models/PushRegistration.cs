using PlantHunter.Web.NotificationHubs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlantHunter.Web.Data.Models
{
    public class PushRegistration
    {
        [Key]
        public string DeviceId { get; set; }
        public MobilePlatform MobilePlatform { get; set; }
        public string Tag { get; set; }
    }
}
