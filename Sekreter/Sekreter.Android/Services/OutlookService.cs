using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Sekreter.Droid.Services;
using Sekreter.Interfaces;
using Xamarin.Forms;

[assembly:Xamarin.Forms.Dependency(typeof(OutlookService))]
namespace Sekreter.Droid.Services
{
    public class OutlookService : Activity,IOutlookService
    {
        [Obsolete]
        public Task<bool> Launch(string stringUri, string to, string cc, string subject, string text)
        {

            //Intent intent = Android.App.Application.Context.PackageManager.GetLaunchIntentForPackage(stringUri);
            //Intent intent = new Intent().SetClassName(stringUri,stringUri+)
            Intent intent = new Intent(Intent.ActionSend);

            try
            {





                if (intent != null)
                {
                    //intent.SetAction(Intent.ActionSendto);
                    //intent = new Intent(Intent.ActionSend);

                    intent.SetPackage(stringUri);

                    intent.SetData(Android.Net.Uri.Parse("mailto:"));
                    intent.SetType("message/rfc822");
                    intent.PutExtra(Intent.ExtraEmail, new string[] { to });
                    intent.PutExtra(Intent.ExtraCc, cc);
                    intent.PutExtra(Intent.ExtraSubject, subject);
                    intent.PutExtra(Intent.ExtraText, text);
                    intent.AddFlags(ActivityFlags.NewTask);
                    Forms.Context.StartActivity(intent);



                }
                else
                {
                    intent = new Intent(Intent.ActionView);
                    intent.AddFlags(ActivityFlags.NewTask);
                    intent.SetData(Android.Net.Uri.Parse("market://details?id=" + stringUri)); // This launches the PlayStore and search for the app if it's not installed on your device
                    Forms.Context.StartActivity(intent);
                }
                return Task.FromResult(true);
            }
            catch (Exception)
            {

                return Task.FromResult(false);
            }
        }


    }
}