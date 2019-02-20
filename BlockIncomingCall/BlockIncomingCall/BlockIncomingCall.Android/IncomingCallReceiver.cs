using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using BlockIncomingCall.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(IncomingCallReceiver))]
namespace BlockIncomingCall.Droid
{
    [BroadcastReceiver()]
    [IntentFilter(new[] { "android.intent.action.PHONE_STATE" })]
    public class IncomingCallReceiver : BroadcastReceiver
    {

        public PersonDatabase memberDatabase;

        private void usingAPI()
        {
            string url = "http://localhost/android/getData.php";
            Task<JsonValue> Task = FetchWeatherAsync(url);
        }


        private async Task<JsonValue> FetchWeatherAsync(string url)
        {
            string str = "";

            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());
                    str += jsonDoc.ToString();
                    Toast.MakeText(Application.Context, str, ToastLength.Long).Show();
                    // Return the JSON document:
                    return jsonDoc;
                }
            }
        }




        public override void OnReceive(Context context, Intent intent)
        {

            memberDatabase = new PersonDatabase();
            var members = memberDatabase.GetMembers();
            if (intent.Extras != null)
            {

                string state = intent.GetStringExtra(TelephonyManager.ExtraState);
                if (state.Equals(TelephonyManager.ExtraStateRinging))
                {



                    String incomingNumber = intent.GetStringExtra(TelephonyManager.ExtraIncomingNumber);
                    // Toast.MakeText(context, "Co cuoc goi den " + incomingNumber, ToastLength.Long).Show();


                    foreach (Person per in members)
                    {
                        if (incomingNumber.Equals(per.Phone))
                        {
                            var manager = (TelephonyManager)context.GetSystemService(Context.TelephonyService);

                            IntPtr TelephonyManager_getITelephony = JNIEnv.GetMethodID(
                                    manager.Class.Handle,
                                    "getITelephony",
                                    "()Lcom/android/internal/telephony/ITelephony;");

                            IntPtr telephony = JNIEnv.CallObjectMethod(manager.Handle, TelephonyManager_getITelephony);
                            IntPtr ITelephony_class = JNIEnv.GetObjectClass(telephony);
                            IntPtr ITelephony_endCall = JNIEnv.GetMethodID(
                                    ITelephony_class,
                                    "endCall",
                                    "()Z");
                            JNIEnv.CallBooleanMethod(telephony, ITelephony_endCall);
                            JNIEnv.DeleteLocalRef(telephony);
                            JNIEnv.DeleteLocalRef(ITelephony_class);


                            Toast.MakeText(context, incomingNumber + " was blocked", ToastLength.Long).Show();
                            //   usingAPI();
                        }
                    }

                }
                if ((state.Equals(TelephonyManager.ExtraStateOffhook)))
                {
                    // Cuộc gọi bắt đầu
                }
                if (state.Equals(TelephonyManager.ExtraStateIdle))
                {
                    // Cuộc gọi kết thúc

                }


            }
        }
    }
}