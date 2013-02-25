using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using CrossBar.Platform.ViewModels;
using CrossBar.UI.Touch.Controllers;
using Cirrious.MvvmCross.Views;
using Cirrious.MvvmCross.Binding.Touch.ExtensionMethods;
using System.Collections.Generic;

namespace Views
{
    public partial class BreweryView : ViewControllerBase<BreweryViewModel>
    {
        public BreweryView(MvxShowViewModelRequest request) 
            : base (request, "BreweryView", null)
        {
        }
		
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            Title = "Brewery";
            
            this.AddBindings(new Dictionary<object, string> ()
            {
                { Name, "Text Brewery.Name" },
                { Url, "Title Brewery.Url" },
                { Favorite, "IsFavorite IsFavorite; TouchUpInside ToggleFavoriteCommand" }
            });
        }
    }
}

