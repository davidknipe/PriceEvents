using EPiServer.ServiceLocation;
using Mediachase.Commerce.Pricing;
using PriceEvents.Helpers;
using PriceEvents.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PriceEvents.Events
{
    /// <summary>
    /// Event handler for subscribing to and broadcasting price change events
    /// </summary>
    public class PriceEventsHandler : IPriceEvents, IDisposable
    {
        private static PriceEventsHandler _instance;
        private static object _keyLock = new object();

        private Dictionary<string, object> _eventKeys = new Dictionary<string, object>();
        private EventHandlerList _events = new EventHandlerList();

        public event EventHandler<PriceEventArgs> PriceChanged
        {
            add
            {
                this.Events.AddHandler(this.GetEventKey(PriceEventResources.PRICE_CHANGED_EVENT), value);
            }
            remove
            {
                this.Events.RemoveHandler(this.GetEventKey(PriceEventResources.PRICE_CHANGED_EVENT), value);
            }
        }

        internal virtual void RaisePriceChanged(object sender, string userName, IEnumerable<IPriceValue> prices)
        {
            EventHandler<PriceEventArgs> handler = 
                this.Events[this.GetEventKey(PriceEventResources.PRICE_CHANGED_EVENT)] as EventHandler<PriceEventArgs>;
            if (handler != null)
            {
                handler(this, new PriceEventArgs() { User = userName, PriceValues = prices });
            }
        }

        public event EventHandler<PriceDeletingEventArgs> PriceDeleting
        {
            add
            {
                this.Events.AddHandler(this.GetEventKey(PriceEventResources.PRICE_DELETING_EVENT), value);
            }
            remove
            {
                this.Events.RemoveHandler(this.GetEventKey(PriceEventResources.PRICE_DELETING_EVENT), value);
            }
        }

        internal virtual void RaisePriceDeleting(object sender, string userName, IEnumerable<long> priceValueIds)
        {
            EventHandler<PriceDeletingEventArgs> handler = 
                this.Events[this.GetEventKey(PriceEventResources.PRICE_DELETING_EVENT)] as EventHandler<PriceDeletingEventArgs>;
            if (handler != null)
            {
                handler(this, new PriceDeletingEventArgs() { User = userName, PriceValueIds = priceValueIds });
            }
        }

        public static PriceEventsHandler Instance
        {
            get
            {
                return (_instance ?? (_instance = new PriceEventsHandler()));
            }
        }

        private object GetEventKey(string eventKey)
        {
            object key;
            if (!this._eventKeys.TryGetValue(eventKey, out key))
            {
                lock (_keyLock)
                {
                    if (!this._eventKeys.TryGetValue(eventKey, out key))
                    {
                        key = new object();
                        this._eventKeys[eventKey] = key;
                    }
                }
            }
            return key;
        }

        private EventHandlerList Events
        {
            get
            {
                if (this._events == null)
                {
                    throw new ObjectDisposedException(base.GetType().FullName);
                }
                return this._events;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._events != null)
                {
                    this._events.Dispose();
                    this._events = null;
                }
                if (object.ReferenceEquals(this, _instance))
                {
                    _instance = null;
                }
            }
        }

    }
}


