using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceEvents.Events
{
    public class PriceDeletingEventArgs
    {
        public string User { get; set; }
        public IEnumerable<long> PriceValueIds { get; set; }
    }
}
