using Castle.DynamicProxy;
using Mediachase.Commerce.Pricing;
using Mediachase.Commerce.Security;
using PriceEvents.Events;
using System.Collections.Generic;
using System.Linq;

namespace PriceEvents.Interceptors
{
    /// <summary>
    /// Interceptor for <see cref="IPriceService"/>. Will listen in for the Save event and broadcaset Price changed events
    /// </summary>
    public class PriceServiceInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();

            if (invocation.MethodInvocationTarget.Name == "SetCatalogEntryPrices")
            {
                this.broadcastEvent(invocation.Arguments);
            }
        }

        private void broadcastEvent(object[] methodArguments)
        {
            //Expecting a method arguement where the first item is IEnumerable<IPriceDetailValue> 
            if (methodArguments != null && methodArguments.Length == 2 && methodArguments[1] != null)
            {
                var priceValues = methodArguments[1] as IEnumerable<IPriceDetailValue>;
                if (priceValues != null)
                {
                    var prices = priceValues.ToList().ConvertAll<IPriceValue>(x => x as IPriceValue);
                    PriceEventsHandler.Instance.RaisePriceChanged(this, new UsernameHelper().GetCurrentUsername(), priceValues);
                }
            }
        }
    }
}