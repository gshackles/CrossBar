using System;
using System.Collections.Generic;
using System.Linq;
using Amarillo;
using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.Dialog.Touch;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Touch.Interfaces;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using CrossBar.Platform;
using CrossBar.Platform.IoC;
using CrossBar.Platform.Messaging;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Binding.Binders;
using CrossBar.Platform.Converters;

namespace CrossBar.UI.Touch
{
	public class Setup : MvxTouchDialogBindingSetup
	{
		public Setup(MvxApplicationDelegate applicationDelegate, IMvxTouchViewPresenter presenter)
			: base(applicationDelegate, presenter)
		{
		}
		
		protected override MvxApplication CreateApp()
		{
			return new CrossBarApplication();
		}

		protected override void InitializeIoC()
		{
			base.InitializeIoC ();

			var client = new AmarilloClient (null);
			
			this.RegisterServiceType<IErrorReporter, ErrorReporter>();
			this.RegisterServiceInstance<IAmarilloClient>(client);
			
			ContainerBootstrapper.Initialize(this);
		}

		protected override void AddPluginsLoaders(Cirrious.MvvmCross.Platform.MvxLoaderPluginRegistry loaders)
		{
			loaders.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.DownloadCache.Touch.Plugin>();
			loaders.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.File.Touch.Plugin>();
			
			base.AddPluginsLoaders (loaders);
		}
		
		protected override Cirrious.MvvmCross.Interfaces.IoC.IMvxIoCProvider CreateIocProvider()
		{
			return new TinyIoCProvider();
		}
		
		protected override void InitializeDefaultTextSerializer()
		{
			Cirrious.MvvmCross.Plugins.Json.PluginLoader.Instance.EnsureLoaded(true);
		}

		protected override void FillValueConverters (Cirrious.MvvmCross.Binding.Interfaces.Binders.IMvxValueConverterRegistry registry)
		{
			base.FillValueConverters (registry);

			registry.AddOrOverwrite ("CollectionEmptyConverter", new CollectionEmptyConverter());
		}
	}
}
