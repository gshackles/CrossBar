using Amarillo;
using Android.Content;
using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.Binding.Droid;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Plugins.Sqlite;
using Cirrious.MvvmCross.Plugins.Sqlite.Droid;
using CrossBar.Platform;
using CrossBar.Platform.DataAccess.Repositories;
using CrossBar.Platform.IoC;
using CrossBar.Platform.Messaging;

namespace CrossBar.UI.Android
{
    public class Setup
        : MvxBaseAndroidBindingSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override MvxApplication CreateApp()
        {
            return new CrossBarApplication();
        }

        protected override void InitializeIoC()
        {
            base.InitializeIoC();

            var client = new AmarilloClient(null);

            this.RegisterServiceType<IErrorReporter, ErrorReporter>();
            this.RegisterServiceInstance<IAmarilloClient>(client);
            this.RegisterServiceType<ISQLiteConnectionFactory, MvxDroidSQLiteConnectionFactory>();
            this.RegisterServiceType<IFavoriteBeerRepository, FavoriteBeerRepository>();
            this.RegisterServiceType<IFavoriteBreweryRepository, FavoriteBreweryRepository>();

            ContainerBootstrapper.Initialize(this);
        }

        protected override Cirrious.MvvmCross.Interfaces.IoC.IMvxIoCProvider CreateIocProvider()
        {
            return new TinyIoCProvider();
        }

        protected override void InitializeDefaultTextSerializer()
        {
            Cirrious.MvvmCross.Plugins.Json.PluginLoader.Instance.EnsureLoaded(true);
        }
    }
}