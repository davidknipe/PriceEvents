using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceEvents.Events
{
    public class PriceDeletingEventArgs
    {
        /// <summary>
        /// Gets or sets the User
        /// </summary>
        /// <value>string</value>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the PriceValueIds
        /// </summary>
        /// <value>List of price value ids</value>
        public IEnumerable<long> PriceValueIds { get; set; }
    }
}
