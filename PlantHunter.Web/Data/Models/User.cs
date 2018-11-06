using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantHunter.Web.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string DeviceId { get; set; }
        public string PushNotificationId { get; set; }
        public string Pns { get; set; }
    }
}
