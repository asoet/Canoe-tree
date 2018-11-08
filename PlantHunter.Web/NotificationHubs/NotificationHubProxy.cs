using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.NotificationHubs.Messaging;
using Microsoft.EntityFrameworkCore;
using PlantHunter.Web.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web.Data;
using web;
using Microsoft.Extensions.Options;

namespace PlantHunter.Web.NotificationHubs
{
    public class NotificationHubProxy
    {
        //private NotificationHubClient _hubClient;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly AppSettings _settings;

        public NotificationHubProxy(ApplicationDbContext applicationDbContext, IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
            _applicationDbContext = applicationDbContext;
            //_hubClient = NotificationHubClient.CreateClientFromConnectionString(NotificationHubConfiguration.ConnectionString, NotificationHubConfiguration.HubName);
        }

        /// 
        /// <summary>
        /// Register device to receive push notifications. 
        /// Registration ID ontained from Azure Notification Hub has to be provided
        /// Then basing on platform (Android, iOS or Windows) specific
        /// handle (token) obtained from Push Notification Service has to be provided
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deviceUpdate"></param>
        /// <returns></returns>
        public async Task<HubResponse> RegisterForPushNotifications(string id, DeviceRegistration deviceUpdate)
        {
          
                var device = await _applicationDbContext.PushRegistrations.FirstOrDefaultAsync(f => f.DeviceId == deviceUpdate.DeviceId);
                if(device == null)
                {
                    _applicationDbContext.PushRegistrations.Add(new Data.Models.PushRegistration { DeviceId = deviceUpdate.DeviceId, MobilePlatform = deviceUpdate.Platform, Tag = deviceUpdate.Tags.First() });
                }
                else
                {
                    device.Tag = deviceUpdate.Tags.First();
                }
                await _applicationDbContext.SaveChangesAsync();
                return new HubResponse();
           
        }

        /// 
        /// <summary>
        /// Send push notification to specific platform (Android, iOS or Windows)
        /// </summary>
        /// <param name="newNotification"></param>
        /// <returns></returns>
        public async Task<HubResponse<NotificationOutcome>> SendNotification(PushNotification newNotification)
        {
            string title = "Plant update";
            string body = newNotification.Content;
            var data = new { action = "Play", userId = 5 };
            var pushSent = PushNotificationLogic.SendPushNotification(newNotification.Tags, title, body, data, _settings.FireBaseKey);

            return new HubResponse<NotificationOutcome>();

            //try
            //{
            //    NotificationOutcome outcome = null;

            //    switch (newNotification.Platform)
            //    {
            //        case MobilePlatform.wns:
            //            if (newNotification.Tags == null)
            //                outcome = await _hubClient.SendWindowsNativeNotificationAsync(newNotification.Content);
            //            else
            //                outcome = await _hubClient.SendWindowsNativeNotificationAsync(newNotification.Content, newNotification.Tags);
            //            break;
            //        case MobilePlatform.apns:
            //            if (newNotification.Tags == null)
            //                outcome = await _hubClient.SendAppleNativeNotificationAsync(newNotification.Content);
            //            else
            //                outcome = await _hubClient.SendAppleNativeNotificationAsync(newNotification.Content, newNotification.Tags);
            //            break;
            //        case MobilePlatform.gcm:
            //            if (newNotification.Tags == null)
            //                outcome = await _hubClient.SendGcmNativeNotificationAsync(newNotification.Content);
            //            else
            //                outcome = await _hubClient.SendGcmNativeNotificationAsync(newNotification.Content, newNotification.Tags);
            //            break;
            //    }

            //    if (outcome != null)
            //    {
            //        if (!((outcome.State == NotificationOutcomeState.Abandoned) ||
            //            (outcome.State == NotificationOutcomeState.Unknown)))
            //            return new HubResponse<NotificationOutcome>();
            //    }

            //    return new HubResponse<NotificationOutcome>().SetAsFailureResponse().AddErrorMessage("Notification was not sent due to issue. Please send again.");
        //}
        }
    }
}
