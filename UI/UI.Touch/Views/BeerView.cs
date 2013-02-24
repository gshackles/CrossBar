using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using CrossBar.Platform.ViewModels;
using CrossBar.UI.Touch.Controllers;
using Cirrious.MvvmCross.Views;
using Cirrious.MvvmCross.Binding.Touch.ExtensionMethods;
using System.Collections.Generic;

namespace CrossBar.UI.Views
{
    [Register ("BeerView")]
    public partial class BeerView : ViewControllerBase<BeerViewModel>
    {
        public BeerView(MvxShowViewModelRequest request) 
            : base (request, "BeerView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			
            Title = "Beer";

            this.AddBindings (new Dictionary<object, string> ()
            {
                { Name, "Text Beer.Name" },
                { Description, "Text Beer.Description" },
                { ABV, "Text Beer.ABV" },
                { BreweryName, "Title Beer.Brewery.Name; TouchUpInside SelectBreweryCommand" },
                //{ Favorite, "CurrentImage IsFavorite, Converter=FavoriteButtonImageConverter; TouchUpInside ToggleFavoriteCommand" }
                { Favorite, "Text IsFavorite; TouchUpInside ToggleFavoriteCommand" }
            });
        }
    }
}

