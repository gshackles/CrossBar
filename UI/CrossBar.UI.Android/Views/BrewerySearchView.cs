using Android.App;
using Cirrious.MvvmCross.Binding.Droid.Views;
using CrossBar.Platform.ViewModels;

namespace CrossBar.UI.Android.Views
{
    [Activity(Label = "Find Breweries")]
    public class BrewerySearchView : MvxBindingActivityView<BrewerySearchViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.BrewerySearch);
        }
    }
}