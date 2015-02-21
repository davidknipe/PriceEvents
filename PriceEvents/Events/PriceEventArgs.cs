using Mediachase.Commerce.Pricing;
using System;
using System.Collections.Generic;

namespace PriceEvents.Events
{
    /// <summary>
    /// Price changed event arguments
    /// </summary>
    public class PriceEventArgs : EventArgs
    {
        public string User { get; set; }
        public IEnumerable<IPriceValue> PriceValues { get; set; }
    }
}
