using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TchospiraApp.Authentication
{
    public interface IAuthentication
    {
        Task<MobileServiceUser> LoginASync(MobileServiceClient client, 
            MobileServiceAuthenticationProvider provider,
            IDictionary<string, string> parameters = null);

        Task<MobileServiceUser> LogoutASync(MobileServiceClient client,
            MobileServiceAuthenticationProvider provider,
            IDictionary<string, string> parameters = null);
    }
}
