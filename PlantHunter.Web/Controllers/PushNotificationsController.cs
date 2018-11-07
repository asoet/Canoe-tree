using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.NotificationHubs;
using Microsoft.EntityFrameworkCore;
using PlantHunter.Web.NotificationHubs;
using web.Data;

namespace PlantHunter.Web.Controllers
{
    /// 
    /// <summary>
    /// Anonymous access is only for testing purposes
    /// Remember to enable authentication
    /// </summary>
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/notifications")]
    public class PushNotificationsController : Controller
    {
        private NotificationHubProxy _notificationHubProxy;
        private readonly ApplicationDbContext _context;

        public PushNotificationsController(ApplicationDbContext context)
        {
            _context = context;
            _notificationHubProxy = new NotificationHubProxy(_context);
        }

        ///// 
        ///// <summary>
        ///// Get registration ID
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("register")]
        //public async Task<IActionResult> CreatePushRegistrationId()
        //{
        //    var registrationId = await _notificationHubProxy.CreateRegistrationId();
        //    return Ok(registrationId);
        //}

        ///// 
        ///// <summary>
        ///// Delete registration ID and unregister from receiving push notifications
        ///// </summary>
        ///// <param name="registrationId"></param>
        ///// <returns></returns>
        //[HttpDelete("unregister/{registrationId}")]
        //public async Task<IActionResult> UnregisterFromNotifications(string registrationId)
        //{
        //    await _notificationHubProxy.DeleteRegistration(registrationId);
        //    return Ok();
        //}

        /// 
        /// <summary>
        /// Register to receive push notifications
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deviceUpdate"></param>
        /// <returns></returns>
        [HttpPut("enable/{id}")]
        public async Task<IActionResult> RegisterForPushNotifications(string id, [FromBody] DeviceRegistration deviceUpdate)
        {
            var device = await _context.PushRegistrations.FirstOrDefaultAsync(f => f.DeviceId == deviceUpdate.DeviceId);
            if (device == null)
            {
                _context.PushRegistrations.Add(new Data.Models.PushRegistration { DeviceId = deviceUpdate.DeviceId, MobilePlatform = deviceUpdate.Platform, Tag = deviceUpdate.Tags.First() });
            }
            else
            {
                device.Tag = deviceUpdate.Tags.First();
                _context.Update(device);
            }
            await _context.SaveChangesAsync();
            //HubResponse registrationResult = await _notificationHubProxy.RegisterForPushNotifications(id, deviceUpdate);
            //if (registrationResult.CompletedWithSuccess)
            return Ok();
            //return BadRequest("An error occurred while sending push notification: " + registrationResult.FormattedErrorMessages);
        }

    }
}