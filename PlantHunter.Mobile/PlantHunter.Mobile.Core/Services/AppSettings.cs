// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using Xamarin.Essentials;

namespace PlantHunter.Mobile.Core.Services
{
    public class AppSettings : IAppSettings
    {
        public const string ApiUrlKey = "ApiUrlKey";
        public const string ApiUrlDefaultValue = "https://planthunter-2-dev-as.azurewebsites.net/";

        public string ApiUrl
        {
            get { return Preferences.Get(ApiUrlKey, ApiUrlDefaultValue); }
            set { Preferences.Set(ApiUrlKey, value); }
        }

        public const string RoleKey = "RoleKey";
        public const string RoleDefaultValue = "Hiker";

        public string Role
        {
            get { return Preferences.Get(RoleKey, RoleDefaultValue); }
            set { Preferences.Set(RoleKey, value); }
        }

        public const string PushRegistrationIdKey = "PushRegistrationIdKey";
        public const string PushRegistrationIdDefaultValue = "";

        public string PushRegistrationId
        {
            get { return Preferences.Get(PushRegistrationIdKey, PushRegistrationIdDefaultValue); }
            set { Preferences.Set(PushRegistrationIdKey, value); }
        }
        
    }
}
