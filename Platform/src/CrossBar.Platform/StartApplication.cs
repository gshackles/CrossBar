using Cirrious.MvvmCross.Interfaces.ViewModels;
using Cirrious.MvvmCross.ViewModels;
using CrossBar.Platform.ViewModels;

namespace CrossBar.Platform
{
    public class StartApplication : MvxApplicationObject, IMvxStartNavigation
    {
        public void Start()
        {
            RequestNavigate<MainMenuViewModel>();
        }

        public bool ApplicationCanOpenBookmarks { get { return true; } }
    }
}