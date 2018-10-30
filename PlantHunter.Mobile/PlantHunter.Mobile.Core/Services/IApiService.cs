using System.IO;
using System.Threading.Tasks;

namespace PlantHunter.Mobile.Core.Services
{
    public interface IApiService
    {
        Task<bool> UploadPictureAsync(Stream pictureStream, string contentType);
    }
}