using Android.App;
using Android.Nfc;
using PlantHunter.Mobile.Core.Services.NFC;
using PlantHunter.Mobile.Droid.Services.NFC;

[assembly: Xamarin.Forms.Dependency(typeof(NfcService))]
namespace PlantHunter.Mobile.Droid.Services.NFC
{
    public class NfcService : INfcService
    {
        NfcAdapter nfcDevice;

        public NfcService()
        {
            var activity = ((Activity)Xamarin.Forms.Forms.Context);
            nfcDevice = NfcAdapter.GetDefaultAdapter(activity);
        }

        public bool IsAvailable
        {
            get
            {
                return nfcDevice?.IsEnabled == true && nfcDevice.IsNdefPushEnabled;
            }
        }
    }
}