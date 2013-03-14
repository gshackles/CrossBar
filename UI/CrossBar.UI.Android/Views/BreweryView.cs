using Android.App;
using Cirrious.MvvmCross.Binding.Droid.Views;
using CrossBar.Platform.ViewModels;

namespace CrossBar.UI.Android.Views
{
    [Activity(Label = "Brewery")]
    public class BreweryView : MvxBindingActivityView<BreweryViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.Brewery);
        }
    }
}