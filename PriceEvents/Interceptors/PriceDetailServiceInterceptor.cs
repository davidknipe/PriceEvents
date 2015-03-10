using Castle.DynamicProxy;
using Mediachase.Commerce.Pricing;
using Mediachase.Commerce.Security;
using PriceEvents.Events;
using PriceEvents.Helpers;
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
        private const int ARGUMENT_INDEX = 0;

        public void Intercept(IInvocation invocation)
        {
            if (invocation.MethodInvocationTarget.Name == "Delete")
            {
                this.BroadcastPriceDeletingEvent(invocation.Arguments);
            }

            invocation.Proceed();

            if (invocation.MethodInvocationTarget.Name == "Save")
            {
                this.BroadcastPriceChangedEvent(invocation.Arguments);
            }
        }

        private void BroadcastPriceChangedEvent(object[] methodArguments)
        {
            if (!PriceEventHelpers.IsMethodArgumentsValid(methodArguments, ARGUMENT_INDEX))
                return;

            var priceValues = methodArguments.FirstOrDefault() as IEnumerable<IPriceDetailValue>;

            if (priceValues == null)
                return;

            var prices = priceValues.ToList().ConvertAll<IPriceValue>(x => x as IPriceValue);
            PriceEventsHandler.Instance.RaisePriceChanged(this, PriceEventHelpers.GetCurrentUsername(),
                prices);
        }

        private void BroadcastPriceDeletingEvent(object[] methodArguments)
        {
            if (!PriceEventHelpers.IsMethodArgumentsValid(methodArguments, ARGUMENT_INDEX))
                return;

            var priceValueIds = methodArguments.FirstOrDefault() as IEnumerable<long>;

            if (priceValueIds == null)
                return;

            PriceEventsHandler.Instance.RaisePriceDeleting(this, PriceEventHelpers.GetCurrentUsername(),
                priceValueIds);
        }
    }
}