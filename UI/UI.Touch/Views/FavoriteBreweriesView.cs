using System;
using CrossBar.UI.Touch.Controllers;
using CrossBar.Platform.ViewModels;
using Cirrious.MvvmCross.Views;
using Cirrious.MvvmCross.Binding.Touch.ExtensionMethods;
using System.Collections.Generic;

namespace CrossBar.UI.Views
{
    public class FavoriteBreweriesView : TableViewControllerBase<FavoriteBreweriesViewModel>
    {
        public FavoriteBreweriesView(MvxShowViewModelRequest request)
            : base(request)
        {
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Favorite Breweries";
            
            var source = new CommandTableViewSource(TableView,
                                                    "FavoriteBreweryCell",
                                                    "TitleText Name",
                                                    ViewModel.SelectFavoriteCommand);
            
            this.AddBindings(new Dictionary<object, string>()
            {
                { source, "ItemsSource FavoriteBreweries" }
            });
            
            TableView.Source = source;
        }
    }
}
