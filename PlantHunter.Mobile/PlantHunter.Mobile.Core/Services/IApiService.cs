using PlantHunter.Mobile.Core.Models;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PlantHunter.Mobile.Core.Services
{
    public interface IApiService
    {
        Task<bool> UploadPictureAsync(Stream pictureStream, string contentType, AddPictureModel additionalInfo);
        Task<IEnumerable<Plant>> GetAllPlantsAsync();
    }
}