using Castle.DynamicProxy;
using Mediachase.Commerce.Pricing;
using Mediachase.Commerce.Security;
using PriceEvents.Events;
using System.Collections.Generic;
using System.Linq;

namespace PriceEvents.Interceptors
{
    /// <summary>
    /// Interceptor for <see cref="IPriceDetailService"/>. Will listen in for the Save event 
    /// and broadcast events to notifiy users of price changes or deletions
    /// </summary>
public class PriceDetailServiceInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        if (invocation.MethodInvocationTarget.Name == "Delete")
        {
            this.broadcastPriceDeletingEvent(invocation.Arguments);
        }

        invocation.Proceed();

        if (invocation.MethodInvocationTarget.Name == "Save")
        {
            this.broadcastPriceChangedEvent(invocation.Arguments);
        }
    }

        private void broadcastPriceChangedEvent(object[] methodArguments)
        {
            //Expecting a method arguement where the first item is IEnumerable<IPriceDetailValue> 
            if (methodArguments != null && methodArguments.Length == 1 && methodArguments[0] != null)
            {
                var priceValues = methodArguments[0] as IEnumerable<IPriceDetailValue>;
                if (priceValues != null)
                {
                    var prices = priceValues.ToList().ConvertAll<IPriceValue>(x => x as IPriceValue);
                    PriceEventsHandler.Instance.RaisePriceChanged(this, new UsernameHelper().GetCurrentUsername(), prices);
                }
            }
        }

        private void broadcastPriceDeletingEvent(object[] methodArguments)
        {
            //Expecting a method arguement where the first item is IEnumerable<long> 
            if (methodArguments != null && methodArguments.Length == 1 && methodArguments[0] != null)
            {
                var priceValueIds = methodArguments[0] as IEnumerable<long>;
                if (priceValueIds != null)
                {
                    PriceEventsHandler.Instance.RaisePriceDeleting(this, new UsernameHelper().GetCurrentUsername(), priceValueIds);
                }
            }
        }
    }
}