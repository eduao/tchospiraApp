using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Gcm.Client;
using Android.Util;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Android.Support.V4.App;
using Android.Media;

namespace TchospiraApp.Droid
{
    [BroadcastReceiver(Permission = Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_MESSAGE },
     Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK },
     Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_LIBRARY_RETRY },
     Categories = new string[] { "@PACKAGE_NAME@" })]
    public class GcmBroadcastReceiver : GcmBroadcastReceiverBase<GcmService>
    {
        //IMPORTANT: Change this to your own Sender ID!
        //The SENDER_ID is your Google API Console App Project Number
        public static string[] SENDER_IDS = new string[] { "299538139111" };
    }

    [Service] //Must use the service tag
    public class GcmService : GcmServiceBase
    {
        public GcmService() : base(GcmBroadcastReceiver.SENDER_IDS) { }

        MobileServiceClient client = new MobileServiceClient(TchospiraApp.Helpers.Settings.UrlSite);

        public static string RegistrationId { get; private set; }

        protected override void OnRegistered(Context context, string registrationId)
        {
            //Receive registration Id for sending GCM Push Notifications to
            Log.Verbose("PushHandlerBroadcastReceiver", "GCM Registered:    " + registrationId);
            RegistrationId = registrationId;
            var push = client.GetPush();
            MainActivity.CurrentActivity.RunOnUiThread(() => Register(push, null));
            
        }

        public async void Register(Microsoft.WindowsAzure.MobileServices.Push push, IEnumerable<string> tags)
        {
            try
            {
                const string templateBodyGCM = "\"data\":{\"message\":\"$(messageParam)\"}}";
                JObject templates = new JObject();
                templates["genericMessage"] = new JObject
                {
                    {"body", templateBodyGCM }
                };
                await push.RegisterAsync(RegistrationId, templates);
                Log.Info("Push installation ID", push.InstallationId.ToString());
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Debugger.Break();
            }
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            //Receive notice that the app no longer wants notifications
            Log.Error("PushHandlerBroadcastReceiver", "cancelamento id : " + registrationId);
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            Log.Info("Recebido por broadcast", "GCM Recebido");

            var msg = new StringBuilder();
            //Push Notification arrived - print out the keys/values
            if (intent == null || intent.Extras == null)
                foreach (var key in intent.Extras.KeySet())
                    msg.AppendLine(key + " = " + intent.Extras.Get(key).ToString());
            //Console.WriteLine("Key: {0}, Value: {1}");
            var pref = GetSharedPreferences(context.PackageName, FileCreationMode.Private);
            var edit = pref.Edit();
            edit.PutString("last_msg", msg.ToString());
            edit.Commit();


            string message = intent.Extras.GetString("message");

            if (!string.IsNullOrEmpty(message))
            {
                CreateNotification("Push", message);
                return;
            }

            string msg2 = intent.Extras.GetString("msg");


            if (!string.IsNullOrEmpty(msg2))
            {
                CreateNotification("New Hub message", msg2);
                return;
            }

            CreateNotification("Detalhes de mensagem desconhecidos", msg.ToString());
        }

        //protected override bool OnRecoverableError(Context context, string errorId)
        //{
        //    //Some recoverable error happened
        //}

        protected override void OnError(Context context, string errorId)
        {
            //Some more serious error happened
            Log.Error("PushHandlerBroadcastReceiver", "GCM error : " + errorId);
        }

        private void CreateNotification(string title, string desc)
        {
            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

            var uiIntent = new Intent(this, typeof(MainActivity));

            NotificationCompat.Builder builder = new NotificationCompat.Builder(this);

            var notification = builder.SetContentIntent(PendingIntent.GetActivities(this, 0, uiIntent, 0))
                .SetSmallIcon(Android.Resource.Drawable.SymActionEmail)
                .SetTicker(title)
                .SetContentText(desc)
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                .SetAutoCancel(true).Build();

            notificationManager.Notify(1, notification);
        }
    }
}