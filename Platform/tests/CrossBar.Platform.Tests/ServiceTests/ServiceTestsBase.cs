using System;
using System.Collections.Generic;
using System.Linq;
using Amarillo;
using CrossBar.Platform.IoC;
using CrossBar.Platform.Messaging;
using CrossBar.Platform.Messaging.Messages;
using CrossBar.Platform.Tests.Mocks;
using NUnit.Framework;
using TinyMessenger;
using Cirrious.MvvmCross.Platform;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Core;

namespace CrossBar.Platform.Tests.ServiceTests
{
    [TestFixture]
    public abstract class ServiceTestsBase<TService> : IMvxServiceProducer
        where TService : class
    {
        protected MockAmarilloClient Client { get; private set; }
        protected ITinyMessengerHub MessengerHub { get; private set; }
        protected TService Service { get; private set; }
        protected MockErrorReporter ErrorReporter { get; private set; }
        
        [SetUp]
        public void SetUp()
        {
            MvxSingleton.ClearAllSingletons();

            Client = new MockAmarilloClient();

            var container = new TinyIoCProvider();
            var serviceProvider = new MvxServiceProvider(container);

            ErrorReporter = new MockErrorReporter();
            
            container.RegisterServiceInstance<IMvxServiceProviderRegistry>(serviceProvider);
            container.RegisterServiceInstance<IMvxServiceProvider>(serviceProvider);
            container.RegisterServiceInstance<IAmarilloClient>(Client);
            container.RegisterServiceInstance<IErrorReporter>(ErrorReporter);

            ContainerBootstrapper.Initialize(this);

            MessengerHub = container.GetService<ITinyMessengerHub>();
            Service = container.GetService<TService>();

            MessengerHub.Subscribe<ErrorMessage>(msg => ErrorReporter.ReportError(msg.Message));
        }
    }
}
