using System;
using Android.App;
using Android.Content;
using Xamarin.Forms;
using MakeCall.Droid;

[assembly: Dependency(typeof(PhoneCall_Droid))]
namespace MakeCall.Droid
{
    public class PhoneCall_Droid : IPhoneCall
    {
        public void MakeQuickCall(string PhoneNumber)
        {
            try
            {
                var uri = Android.Net.Uri.Parse(string.Format("tel:{0}", PhoneNumber));
                var intent = new Intent(Intent.ActionCall, uri);
                Xamarin.Forms.Forms.Context.StartActivity(intent);
            }
            catch (Exception ex)
            {
                new AlertDialog.Builder(Android.App.Application.Context).SetPositiveButton("OK", (sender, args) =>
                {
                    //User pressed OK
                })
                .SetMessage(ex.ToString())
                .SetTitle("Android Exception")
                .Show();
            }
        }
        
    }
}