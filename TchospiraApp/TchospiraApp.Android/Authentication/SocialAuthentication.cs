﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TchospiraApp.Helpers;
using TchospiraApp.Authentication;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using TchospiraApp.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(SocialAuthentication))]
namespace TchospiraApp.Droid
{
    public class SocialAuthentication :IAuthentication
    {
        public async Task<MobileServiceUser> LoginASync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                var user = await client.LoginAsync(Forms.Context, provider);

                Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                Settings.UserId = user?.UserId;

                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}