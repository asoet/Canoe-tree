// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

namespace PlantHunter.Mobile.Core.Services
{
    public interface IAppSettings
    {
        string ApiUrl { get; set; }
        string Role { get; set; }
    }
}