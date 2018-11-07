using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantHunter.Web.NotificationHubs
{
    public class PushNotification : DeviceRegistration
    {
        public string Content { get; set; }
    }
}
