using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Iid;
using PlantHunter.Mobile.Core.Services;
using Plugin.CurrentActivity;

namespace PlantHunter.Mobile.Droid
{
    public class TokenReceiver : ITokenReceiver
    {
        public string GetHandle()
        {
            try
            {

                return FirebaseInstanceId.Instance.Token;
            }
            catch (Exception e)
            {
                throw e;
            }
            return "";
        }
    }
}