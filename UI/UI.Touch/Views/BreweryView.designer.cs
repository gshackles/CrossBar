// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace Views
{
	[Register ("BreweryView")]
	partial class BreweryView
	{
		[Outlet]
		MonoTouch.UIKit.UILabel Name { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton Url { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton Favorite { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Name != null) {
				Name.Dispose ();
				Name = null;
			}

			if (Url != null) {
				Url.Dispose ();
				Url = null;
			}

			if (Favorite != null) {
				Favorite.Dispose ();
				Favorite = null;
			}
		}
	}
}
