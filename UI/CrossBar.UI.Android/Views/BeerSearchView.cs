using Android.App;
using Cirrious.MvvmCross.Binding.Droid.Views;
using CrossBar.Platform.ViewModels;

namespace CrossBar.UI.Android.Views
{
    [Activity(Label = "Find Beers")]
    public class BeerSearchView : MvxBindingActivityView<BeerSearchViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.BeerSearch);
        }
    }
}