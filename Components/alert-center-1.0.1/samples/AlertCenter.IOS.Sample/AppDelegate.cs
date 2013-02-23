using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Controls;

namespace AlertCenterControl.IOS.Sample
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {

        UIWindow window;
        UniversalViewController mainViewController;

        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {
            // create a new window instance based on the screen size
            window = new UIWindow (UIScreen.MainScreen.Bounds);
            
            //mainViewController = new MainViewController ();
            mainViewController = new UniversalViewController ();
            window.RootViewController = mainViewController;
            window.MakeKeyAndVisible ();
		
            return true;
        }
    }
}

