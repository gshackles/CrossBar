using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using Cirrious.MvvmCross.ExtensionMethods;

namespace CrossBar.UI.Touch
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : MvxApplicationDelegate, IMvxServiceConsumer
	{
		private UIWindow _window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			_window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			var presenter = new MvxTouchViewPresenter(this, _window);
			var setup = new Setup(this, presenter);
			setup.Initialize();
			
			this.GetService<IMvxStartNavigation>().Start();

			UINavigationBar.Appearance.TintColor = UIColor.Black;

			_window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

