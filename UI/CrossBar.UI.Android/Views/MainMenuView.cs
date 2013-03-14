using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Cirrious.MvvmCross.Binding.Droid.Views;
using CrossBar.Platform.ViewModels;

namespace CrossBar.UI.Android.Views
{
    [Activity(Label = "CrossBar")]
    public class MainMenuView : MvxBindingActivityView<MainMenuViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.MainMenu);
        }
    }
}