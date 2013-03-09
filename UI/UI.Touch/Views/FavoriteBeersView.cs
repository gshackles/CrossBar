using System;
using CrossBar.UI.Touch.Controllers;
using CrossBar.Platform.ViewModels;
using Cirrious.MvvmCross.Views;
using Cirrious.MvvmCross.Binding.Touch.ExtensionMethods;
using System.Collections.Generic;

namespace CrossBar.UI.Views
{
    public class FavoriteBeersView : TableViewControllerBase<FavoriteBeersViewModel>
    {
        public FavoriteBeersView(MvxShowViewModelRequest request)
            : base(request)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Favorite Beers";

            var source = new CommandTableViewSource(TableView,
                                                    "FavoriteBeerCell",
                                                    "TitleText Name",
                                                    ViewModel.SelectFavoriteCommand);

            this.AddBindings(new Dictionary<object, string>()
            {
                { source, "ItemsSource FavoriteBeers" }
            });

            TableView.Source = source;
        }
    }
}

