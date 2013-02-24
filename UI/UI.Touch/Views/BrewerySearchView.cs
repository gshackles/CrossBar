using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Cirrious.MvvmCross.Binding.Touch.ExtensionMethods;
using Cirrious.MvvmCross.Views;
using CrossBar.Platform.ViewModels;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using CrossBar.UI.Touch.Controllers;
using CrossBar.UI.Touch.Extensions;

namespace CrossBar.UI.Views
{
    [Register ("BrewerySearchView")]
    public partial class BrewerySearchView : ViewControllerBase<BrewerySearchViewModel>
    {
        public BrewerySearchView(MvxShowViewModelRequest request) 
            : base (request, "BrewerySearchView", null)
        {
        }
        
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            
            Title = "Find Breweries";
            
            var source = new CommandTableViewSource (Results, "BreweryCell", "TitleText Name", ViewModel.SelectBreweryCommand);
            Results.Source = source;
            
            this.AddBindings (new Dictionary<object, string> ()
            {
                { Search, "TouchUpInside SearchCommand"}, 
                { Query, "Text Query"},
                { source, "ItemsSource Breweries"},
                { Results, "Hidden Breweries, Converter=CollectionEmptyConverter" }
            });
            
            Query.SetReturnCommand (ViewModel.SearchCommand);
            ViewModel.BindLoadingMessage(View, model => model.IsSearching, "Searching...");
            
            ViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "IsSearching")
                    InvokeOnMainThread (() => Query.ResignFirstResponder ());
                else if (e.PropertyName == "Breweries" && (ViewModel.Breweries != null && ViewModel.Breweries.Count() == 0))
                    this.ShowMessage("No breweries found :(");
            };
        }
    }
}

