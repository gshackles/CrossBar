// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace CrossBar.UI.Views
{
	partial class BeerView
	{
		[Outlet]
		MonoTouch.UIKit.UIButton BreweryName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextView Description { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel Name { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel ABV { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton Favorite { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (BreweryName != null) {
				BreweryName.Dispose ();
				BreweryName = null;
			}

			if (Description != null) {
				Description.Dispose ();
				Description = null;
			}

			if (Name != null) {
				Name.Dispose ();
				Name = null;
			}

			if (ABV != null) {
				ABV.Dispose ();
				ABV = null;
			}

			if (Favorite != null) {
				Favorite.Dispose ();
				Favorite = null;
			}
		}
	}
}
