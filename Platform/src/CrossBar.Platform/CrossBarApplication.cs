using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using CrossBar.Platform.Messaging;
using CrossBar.Platform.Messaging.Messages;
using TinyMessenger;

namespace CrossBar.Platform
{
    public class CrossBarApplication : MvxApplication, IMvxServiceProducer, IMvxServiceConsumer
    {
        public CrossBarApplication()
        {
            var startApplicationObject = new StartApplication();
            this.RegisterServiceInstance<IMvxStartNavigation>(startApplicationObject);

            var errorReporter = this.GetService<IErrorReporter>();
            var hub = this.GetService<ITinyMessengerHub>();

            hub.Subscribe<ErrorMessage>(msg => errorReporter.ReportError(msg.Message));

            Cirrious.MvvmCross.Plugins.WebBrowser.PluginLoader.Instance.EnsureLoaded();
        }

        protected override IMvxViewModelLocator CreateDefaultViewModelLocator()
        {
            return new ViewModelLocator();
        }
    }
}
