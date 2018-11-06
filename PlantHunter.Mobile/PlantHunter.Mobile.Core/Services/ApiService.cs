using Newtonsoft.Json;
using PlantHunter.Mobile.Core.Helpers;
using PlantHunter.Mobile.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PlantHunter.Mobile.Core.Services
{
    public class ApiService : IApiService
    {
        private readonly IAppSettings _appSettings;
        private readonly HttpClient _httpClient;

        public ApiService(IAppSettings appSettings, HttpClient httpClient)
        {
            _appSettings = appSettings;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Plant>> GetAllPlantsAsync()
        {
            _httpClient.BaseAddress = new Uri(_appSettings.ApiUrl);
            var response = await _httpClient.GetAsync($"api/plants");
            if (!response.IsSuccessStatusCode)
                return new List<Plant>();
            return JsonConvert.DeserializeObject<IEnumerable<Plant>>(await response.Content.ReadAsStringAsync());
        }

        public async System.Threading.Tasks.Task<bool> UploadPictureAsync(Stream pictureStream, string contentType, AddPictureModel additionalInfo)
        {
            _httpClient.BaseAddress = new Uri(_appSettings.ApiUrl);
            var response =await  _httpClient.PostAsync($"api/plants", new StringContent(JsonConvert.SerializeObject(new
            {
                ImageBase64 = pictureStream.ConvertToBase64(),
                ContentType = contentType,
                additionalInfo.Latitude,
                additionalInfo.Longitude,
                additionalInfo.Name,
                additionalInfo.DeviceId,
                additionalInfo.EndangeredLevel,
                additionalInfo.Family,
                additionalInfo.ScientificName,
                additionalInfo.Surrounding,
                additionalInfo.Description
            }),Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<string> GetPushRegistrationId()
        {
            string registrationId = string.Empty;
            HttpResponseMessage response = await _httpClient.GetAsync(string.Concat(_appSettings.ApiUrl, "/api/notifications/register"));
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                registrationId = JsonConvert.DeserializeObject<string>(jsonResponse);
            }

            return registrationId;
        }

        public async Task<bool> UnregisterFromNotifications(string registrationId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(string.Concat(_appSettings.ApiUrl, $"/api/notificationsunregister/{registrationId}"));
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<bool> EnablePushNotifications(string id, DeviceRegistration deviceUpdate)
        {
            string registrationId = string.Empty;
            var content = new StringContent(JsonConvert.SerializeObject(deviceUpdate), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(string.Concat(_appSettings.ApiUrl, $"/api/notifications/enable/{id}"), content);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<bool> SendNotification(Notification newNotification)
        {
            var content = new StringContent(JsonConvert.SerializeObject(newNotification), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(string.Concat(_appSettings.ApiUrl, "/api/notifications/send"), content);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

    }
}
