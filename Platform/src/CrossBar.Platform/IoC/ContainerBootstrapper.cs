using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using CrossBar.Platform.Services;
using TinyMessenger;

namespace CrossBar.Platform.IoC
{
    public static class ContainerBootstrapper
    {
        public static void Initialize(IMvxServiceProducer producer)
        {
            producer.RegisterServiceType<ITinyMessengerHub, TinyMessengerHub>();
            producer.RegisterServiceType<ISearchService, SearchService>();
        }
    }
}
