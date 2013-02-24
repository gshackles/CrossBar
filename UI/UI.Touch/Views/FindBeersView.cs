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
using System.Windows.Input;

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

			var source = new CommandTableViewSource (Results, "BeerCell", "TitleText Name", ViewModel.SelectBeerCommand);
			Results.Source = source;

			this.AddBindings (new Dictionary<object, string> ()
            {
				{ Search, "TouchUpInside SearchCommand"}, 
				{ Query, "Text Query"},
				{ source, "ItemsSource Beers"},
				{ Results, "Hidden Beers, Converter=CollectionEmptyConverter" }
			});

			Query.SetReturnCommand (ViewModel.SearchCommand);
			ViewModel.BindLoadingMessage(View, model => model.IsSearching, "Searching...");

			ViewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == "IsSearching")
					InvokeOnMainThread (() => Query.ResignFirstResponder ());
				else if (e.PropertyName == "Beers" && (ViewModel.Beers != null && ViewModel.Beers.Count() == 0))
					this.ShowMessage("No beers found :(");
			};
		}
	}
}

