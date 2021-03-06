using System;
using System.Collections.Generic;
using System.Linq;
using Amarillo;
using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.Binding.Binders;
using Cirrious.MvvmCross.Binding.Bindings.Target.Construction;
using Cirrious.MvvmCross.Dialog.Touch;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Plugins.Sqlite;
using Cirrious.MvvmCross.Plugins.Sqlite.Touch;
using Cirrious.MvvmCross.Touch.Interfaces;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using CrossBar.Platform;
using CrossBar.Platform.Converters;
using CrossBar.Platform.DataAccess.Repositories;
using CrossBar.Platform.IoC;
using CrossBar.Platform.Messaging;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using CrossBar.UI.Bindings;

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
            this.RegisterServiceType<ISQLiteConnectionFactory, MvxTouchSQLiteConnectionFactory>();
            this.RegisterServiceType<IFavoriteBeerRepository, FavoriteBeerRepository>();
            this.RegisterServiceType<IFavoriteBreweryRepository, FavoriteBreweryRepository>();
			
			ContainerBootstrapper.Initialize(this);
		}

		protected override void AddPluginsLoaders(Cirrious.MvvmCross.Platform.MvxLoaderPluginRegistry loaders)
		{
			loaders.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.DownloadCache.Touch.Plugin>();
			loaders.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.File.Touch.Plugin>();
            loaders.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.WebBrowser.Touch.Plugin>();
			
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

			registry.AddOrOverwrite("CollectionEmptyConverter", new CollectionEmptyConverter());
		}

        protected override void FillTargetFactories(Cirrious.MvvmCross.Binding.Interfaces.Bindings.Target.Construction.IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);

            registry.RegisterFactory(new MvxCustomBindingFactory<UIButton>("IsFavorite", button => new FavoriteButtonBinding(button)));
        }
	}
}
