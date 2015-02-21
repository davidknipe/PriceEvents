using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Mediachase.Commerce.Pricing;
using Mediachase.Commerce.Pricing.Database;
using PriceEvents.Events;
using PriceEvents.Interceptors;
using PriceEvents.Interfaces;

namespace PriceEvents.Init
{
    /// <summary>
    /// Initialisation module to configure PriceEvents in EPiServer Commerce
    /// </summary>
    [ModuleDependency(typeof(EPiServer.Commerce.Initialization.InitializationModule))]
    public class PriceEventsInit : IConfigurableModule
    {
        public void Initialize(InitializationEngine context) { }

        public void Preload(string[] parameters) { }

        public void Uninitialize(InitializationEngine context) { }

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Container.Configure(c => c.For<IPriceEvents>()
                .Singleton()
                .Use(PriceEventsHandler.Instance));

            context.Container.Configure(c => c.For<IPriceService>()
                 .Use<PriceServiceDatabase>()
                 .EnrichWith(EnrichmentOf<PriceServiceInterceptor>.ForInterface<IPriceService>)
                 );

            context.Container.Configure(c => c.For<IPriceDetailService>()
                 .Use<PriceDetailDatabase>()
                 .EnrichWith(EnrichmentOf<PriceDetailServiceInterceptor>.ForInterface<IPriceDetailService>)
                 );
        }
    }
}