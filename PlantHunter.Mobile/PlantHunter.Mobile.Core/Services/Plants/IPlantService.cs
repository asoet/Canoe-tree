using MvvmHelpers;
using PlantHunter.Mobile.Core.Models;
using PlantHunter.Mobile.Core.Services.Push;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PlantHunter.Mobile.Core.Services.Plants
{
    public interface IPlantService
    {
        Task<ObservableRangeCollection<Models.Plant>> GetPlantsAsync();
        System.Threading.Tasks.Task<bool> UploadPictureAsync(Stream pictureStream, string contentType, AddPictureModel additionalInfo);
        Task EnablePushNotifications(string handle, DeviceRegistration deviceUpdate);
    }
}
