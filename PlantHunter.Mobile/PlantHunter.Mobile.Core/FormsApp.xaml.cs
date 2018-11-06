// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using MvvmCross;
using MvvmCross.Forms.Core;
using PlantHunter.Mobile.Core.Models;
using PlantHunter.Mobile.Core.Services;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PlantHunter.Mobile.Core
{
    public partial class FormsApp : MvxFormsApplication
    {
        private string _status;
        private string _registrationId;
        private readonly IApiService _apiService;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public string RegistrationId
        {
            get { return _registrationId; }
            set
            {
                _registrationId = value;
                OnPropertyChanged(nameof(_registrationId));
            }
        }

        public FormsApp()
        {
            _apiService = Mvx.Resolve<IApiService>();
            InitializeComponent();

            Device.BeginInvokeOnMainThread(async () =>
            {
                RegistrationId = await _apiService.GetPushRegistrationId();
                Status = "Registration ID obtained: " + RegistrationId;
                var handle = Plugin.AzurePushNotification.CrossAzurePushNotification.Current.Token;

                var deviceUpdate = new DeviceRegistration()
                {
                    Handle = handle,
                    Platform = MobilePlatform.wns,
                    Tags = new string[1] { RegistrationId }
                };

                var result = await _apiService.EnablePushNotifications(RegistrationId, deviceUpdate);
                if (result)
                {
                    Status = "Successfuly enabled push notifications";
                }
                else
                {
                    Status = "Failed to enable push notifications";
                }
            });


            //UnregisterFromPushNotifications = async (parameter) =>
            //{
            //    var result = await _apiService.UnregisterFromNotifications(RegistrationId);
            //    if (result)
            //        Status = "Successfuly unregistered from push notifications";
            //    else
            //        Status = "Failed to unregister from push notifications";
            //};

            //SendPushNotification = new DelegateCommand(async (parameter) =>
            //{
            //    var notification = new PushNotificationsApp.UWP.Model.Notification()
            //    {
            //        Content = "<?xml version=\"1.0\" encoding=\"utf-8\"?><toast><visual><binding template = \"ToastText01\"><text id = \"1\"> Test message </text></binding></visual></toast>"
            //    };

            //    var result = await _restService.SendNotification(notification);
            //    if (result)
            //        Status = "Successfuly sent push notifications";
            //    else
            //        Status = "Failed to send push notifications";
            //});

        }
    }
}