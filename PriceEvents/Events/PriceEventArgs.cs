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
        /// <summary>
        /// Gets or sets the user
        /// </summary>
        /// <value>string</value>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the price values
        /// </summary>
        /// <value>List of price values</value>
        public IEnumerable<IPriceValue> PriceValues { get; set; }
    }
}
