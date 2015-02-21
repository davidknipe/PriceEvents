using Castle.DynamicProxy;
using Mediachase.Commerce.Pricing;
using Mediachase.Commerce.Security;
using PriceEvents.Events;
using System.Collections.Generic;
using System.Linq;

namespace PriceEvents.Interceptors
{
    /// <summary>
    /// Interceptor for <see cref="IPriceDetailService"/>. Will listen in for the Save event and broadcaset Price changed events
    /// </summary>
    public class PriceDetailServiceInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            if (invocation.MethodInvocationTarget.Name == "Save")
            {
                this.broadcastEvent(invocation.Arguments);
            }

            invocation.Proceed();
        }

        private void broadcastEvent(object[] methodArguments)
        {
            //Expecting a method arguement where the first item is IEnumerable<IPriceDetailValue> 
            if (methodArguments != null && methodArguments.Length == 1 && methodArguments[0] != null)
            {
                var priceValues = methodArguments[0] as IEnumerable<IPriceDetailValue>;
                if (priceValues != null)
                {
                    var prices = priceValues.ToList().ConvertAll<IPriceValue>(x => x as IPriceValue);
                    PriceEventsHandler.Instance.RaisePriceSaved(this, new UsernameHelper().GetCurrentUsername(), prices);
                }
            }
        }
    }
}