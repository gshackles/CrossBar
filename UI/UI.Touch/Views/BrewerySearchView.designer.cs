// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace CrossBar.UI.Views
{
	partial class BrewerySearchView
	{
		[Outlet]
		MonoTouch.UIKit.UITextField Query { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton Search { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView Results { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Query != null) {
				Query.Dispose ();
				Query = null;
			}

			if (Search != null) {
				Search.Dispose ();
				Search = null;
			}

			if (Results != null) {
				Results.Dispose ();
				Results = null;
			}
		}
	}
}
