// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using Android.App;
using Android.OS;
using Android.Runtime;
using Firebase;
using Plugin.AzurePushNotification;
using System;

namespace PlantHunter.Mobile.Droid
{
    //You can specify additional application information in this attribute
#if DEBUG
    [Application(Debuggable = true)]
#else
    [Application(Debuggable = false)]
#endif
    [MetaData("com.google.android.maps.v2.API_KEY",
              Value = "AIzaSyC10Pv4N0xvCXqWjK5t9MryzjUu90PnaCU")]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);
            //Set the default notification channel for your app when running Android Oreo
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                //Change for your default notification channel id here
                AzurePushNotificationManager.DefaultNotificationChannelId = "DefaultChannel";

                //Change for your default notification channel name here
                AzurePushNotificationManager.DefaultNotificationChannelName = "General";
            }

            //If debug you should reset the token each time.
#if DEBUG
            AzurePushNotificationManager.Initialize(this, "Endpoint=sb://hacc2018.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=uGcmRtQmCf4aaJTg12P8wx7jcyEpUJxwM19Jxfi1jR0=", "HACC2018", true);
#else
              AzurePushNotificationManager.Initialize(this, "Endpoint=sb://hacc2018.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=uGcmRtQmCf4aaJTg12P8wx7jcyEpUJxwM19Jxfi1jR0=", "HACC2018",false);
#endif

            //Handle notification when app is closed here
            CrossAzurePushNotification.Current.OnNotificationReceived += (s, p) =>
            {


            };
            //A great place to initialize Xamarin.Insights and Dependency Services!
        }

        public override void OnTerminate()
        {
            base.OnTerminate();

            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {

        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {

        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {

        }

        public void OnActivityStarted(Activity activity)
        {

        }

        public void OnActivityStopped(Activity activity)
        {

        }
    }
}