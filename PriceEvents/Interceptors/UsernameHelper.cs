using Mediachase.Commerce.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceEvents.Interceptors
{
    internal class UsernameHelper
    {
        public string GetCurrentUsername()
        {
            if (SecurityContext.Current != null)
            {
                return SecurityContext.Current.CurrentUserName;
            }
            else
            {
                return "UNKNOWN";
            }
        }
    }
}
