using Amarillo;
using Cirrious.MvvmCross.Core;
using Cirrious.MvvmCross.Interfaces.IoC;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Interfaces.Views;
using Cirrious.MvvmCross.Platform;
using Cirrious.MvvmCross.Plugins.Json;
using CrossBar.Platform.IoC;
using CrossBar.Platform.Messaging;
using CrossBar.Platform.Tests.Extensions;
using CrossBar.Platform.Tests.Mocks;
using CrossBar.Platform.ViewModels;
using CrossBar.Platform.ViewModels.Parameters;
using NUnit.Framework;
using TinyMessenger;

namespace CrossBar.Platform.Tests.ViewModelTests
{
    [TestFixture]
    public abstract class ViewModelTestsBase<TViewModel, TParameters> : IMvxServiceProducer
        where TViewModel : ViewModelBase<TParameters>
        where TParameters : ParametersBase
    {
        private IMvxIoCProvider _container;
        protected MockAmarilloClient Client { get; private set; }
        protected ITinyMessengerHub MessengerHub { get; private set; }
        protected MockMvxViewDispatcher Dispatcher { get; private set; }

        [SetUp]
        public void SetUp()
        {
            MvxSingleton.ClearAllSingletons();

            Client = new MockAmarilloClient();

            _container = new TinyIoCProvider();
            var serviceProvider = new MvxServiceProvider(_container);

            _container.RegisterServiceInstance<IMvxServiceProviderRegistry>(serviceProvider);
            _container.RegisterServiceInstance<IMvxServiceProvider>(serviceProvider);
            _container.RegisterServiceInstance<IAmarillo>(Client);
            _container.RegisterServiceInstance<IErrorReporter>(new MockErrorReporter());
            _container.RegisterServiceType<IMvxJsonConverter, MvxJsonConverter>();

            ContainerBootstrapper.Initialize(this);

            Dispatcher = new MockMvxViewDispatcher();
            var mockNavigationProvider = new MockMvxViewDispatcherProvider();
            mockNavigationProvider.Dispatcher = Dispatcher;
            _container.RegisterServiceInstance<IMvxViewDispatcherProvider>(mockNavigationProvider);

            MessengerHub = _container.GetService<ITinyMessengerHub>();
        }

        protected TViewModel GetViewModel(TParameters parameters)
        {
            return GetViewModel<TViewModel, TParameters>(parameters);   
        }

        protected TWantedViewModel GetViewModel<TWantedViewModel, TWantedViewModelParameters>(TWantedViewModelParameters parameters)
            where TWantedViewModelParameters : ParametersBase
            where TWantedViewModel : ViewModelBase<TWantedViewModelParameters>
        {
            var viewModel = _container.GetService<TWantedViewModel>();

            viewModel.Initialize(parameters);
            viewModel.WaitUntilLoaded();

            return viewModel;
        }
    }
}
