using System;
using Android.App;
using Cirrious.MvvmCross.Droid.Views;

namespace CrossBar.UI.Android
{
    [Activity(Label = "CrossBar", MainLauncher = true, NoHistory = true, Icon = "@drawable/icon")]
    public class SplashScreenActivity
        : MvxBaseSplashScreenActivity
    {
        public SplashScreenActivity()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}