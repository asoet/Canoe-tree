
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Plugin.DeviceInfo;
using PlantHunter.Mobile.Core.Services.Plants;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PlantHunter.Mobile.Core.Services.Push
{
    public class TokenReceiver : ITokenReceiver
    {
        readonly IPlantService plantService;

        public TokenReceiver(IPlantService plantService)
        {
            this.plantService = plantService;
        }

        public string SaveToken(string refreshedToken)
        {
            MobilePlatform platform = MobilePlatform.gcm;
            if (Device.RuntimePlatform == Device.Android)
            {
                platform = MobilePlatform.gcm;
            }
            if (Device.RuntimePlatform == Device.iOS)
            {
                platform = MobilePlatform.apns;
            }


            var deviceUpdate = new DeviceRegistration()
            {
                Handle = refreshedToken,
                Platform = platform,
                Tags = new string[1] { refreshedToken },
                DeviceId = CrossDeviceInfo.Current.Id
            };
            Device.BeginInvokeOnMainThread(async () =>
            {
                await plantService.EnablePushNotifications(refreshedToken, deviceUpdate);
            });
            return "refreshedToken";
        }
    }
    public class DeviceRegistration
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public MobilePlatform Platform { get; set; }
        public string Handle { get; set; }
        public string[] Tags { get; set; }
        public string DeviceId { get; set; }
    }

    public enum MobilePlatform
    {
        wns, apns, gcm
    }
}
