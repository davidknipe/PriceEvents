using PriceEvents.Events;
using System;

namespace PriceEvents.Interfaces
{
    /// <summary>
    /// Events exposed for price changes
    /// </summary>
    public interface IPriceEvents
    {
        event EventHandler<PriceEventArgs> PriceChanged;
    }
}
