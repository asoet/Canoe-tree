using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;
using PlantHunter.Mobile.Core.Extensions;
using PlantHunter.Mobile.Core.Models;
using PlantHunter.Mobile.Core.Services.Push;
using PlantHunter.Mobile.Core.Services.Request;

namespace PlantHunter.Mobile.Core.Services.Plants
{
    public class PlantService : IPlantService
    {
        readonly IRequestService requestService;

        public PlantService(IRequestService requestService)
        {
            this.requestService = requestService;
        }

        public async Task<ObservableRangeCollection<Plant>> GetPlantsAsync()
        {
            var builder = new UriBuilder(AppSettings.PlantsEndpoint);
            builder.AppendToPath("api");
            builder.AppendToPath("plants");

            var uri = builder.ToString();
            var plants = await requestService.GetAsync<IEnumerable<Models.Plant>>(uri);

            plants = plants.OrderByDescending(f => f.Points);
            return plants.ToObservableRangeCollection();
        }

        public async System.Threading.Tasks.Task<bool> UploadPictureAsync(Stream pictureStream, string contentType, AddPictureModel additionalInfo)
        {
            var builder = new UriBuilder(AppSettings.PlantsEndpoint);
            builder.AppendToPath("api");
            builder.AppendToPath("plants");
            await requestService.PostAsync(builder.ToString(), new
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
            });
            return true;
        }

        public async Task EnablePushNotifications(string id, DeviceRegistration deviceUpdate)
        {
            var builder = new UriBuilder(AppSettings.PlantsEndpoint);
            builder.AppendToPath("api");
            builder.AppendToPath("notifications");
            builder.AppendToPath("enable");
            builder.AppendToPath(id);
            await requestService.PutAsync(builder.ToString(), deviceUpdate);
        }
    }
}
