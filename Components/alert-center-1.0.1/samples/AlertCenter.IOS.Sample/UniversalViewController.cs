
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Controls;

namespace AlertCenterControl.IOS.Sample
{
    public partial class UniversalViewController : UIViewController
    {
        static bool UserInterfaceIdiomIsPhone {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public UniversalViewController ()
            : base (UserInterfaceIdiomIsPhone ? "UniversalViewController_iPhone" : "UniversalViewController_iPad", null)
        {
        }
        
        public override void DidReceiveMemoryWarning ()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning ();
            
            // Release any cached data, images, etc that aren't in use.
        }
        
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            
            // Perform any additional setup after loading the view, typically from a nib.
        }
        
        public override void ViewDidUnload ()
        {
            base.ViewDidUnload ();
            
            // Clear any references to subviews of the main view in order to
            // allow the Garbage Collector to collect them sooner.
            //
            // e.g. myOutlet.Dispose (); myOutlet = null;
            
            ReleaseDesignerOutlets ();
        }
        
        public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
        {
            // Return true for supported orientations
            if (UserInterfaceIdiomIsPhone) {
                return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
            } else {
                return true;
            }
        }

        partial void SendAlert (NSObject sender)
        {
			AlertCenter.Default.PostMessage ("Download Complete", "\"Puppies Playing.mov\" finished downloading.", UIImage.FromFile ("176.png"));
        }
    }
}

