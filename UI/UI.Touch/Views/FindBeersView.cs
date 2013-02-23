using System;
using System.Drawing;
using Cirrious.MvvmCross.Views;
using CrossBar.Platform.ViewModels;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using CrossBar.UI.Touch.Controllers;
using CrossBar.UI.Touch.Extensions;
using Cirrious.MvvmCross.Binding.Touch.ExtensionMethods;
using System.Collections.Generic;
using Cirrious.MvvmCross.Binding.Touch.Views;
using System.Linq;

namespace CrossBar.UI.Views
{
	[Register ("FindBeersView")]
	public partial class FindBeersView : ViewControllerBase<BeerSearchViewModel>
	{
		public FindBeersView(MvxShowViewModelRequest request) 
			: base (request, "FindBeersView", null)
		{
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Find Beers";

			var source = new MvxSimpleBindableTableViewSource (
				Results, 
                UITableViewCellStyle.Default, 
                new NSString ("BeerCell"),
                "{'TitleText':{'Path':'Name'}}",
           		UITableViewCellAccessory.DisclosureIndicator);
			
			source.SelectionChanged += (sender, e) => 
			{
				ViewModel.SelectBeerCommand.Execute(e.AddedItems[0]);
				Results.DeselectRow(Results.IndexPathForSelectedRow, true);
			};

			this.AddBindings (new Dictionary<object, string> ()
            {
				{ Search, "TouchUpInside SearchCommand"}, 
				{ Query, "Text Query"}, 
				{ source, "{'ItemsSource': {'Path':'Beers'}}"}
			});

			Query.ShouldReturn = field => 
			{
				ViewModel.SearchCommand.Execute(null);
				return true;
			};

			ViewModel.BindLoadingMessage(View, model => model.IsSearching, "Searching...");
			
			Results.Source = source;
			
			ViewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == "IsSearching")
					InvokeOnMainThread (() => Query.ResignFirstResponder ());
				else if (e.PropertyName == "Beers" && ViewModel.Beers.Count() == 0)
					this.ShowMessage("No beers found :(");
			};
		}
	}
}

