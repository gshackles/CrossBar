using Android.App;
using Cirrious.MvvmCross.Binding.Droid.Views;
using CrossBar.Platform.ViewModels;

namespace CrossBar.UI.Android.Views
{
    [Activity(Label = "Beer")]
    public class BeerView : MvxBindingActivityView<BeerViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.Beer);
        }
    }
}