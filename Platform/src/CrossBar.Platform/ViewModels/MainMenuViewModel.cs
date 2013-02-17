using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Cirrious.MvvmCross.Commands;
using CrossBar.Platform.ViewModels.Parameters;
using TinyMessenger;

namespace CrossBar.Platform.ViewModels
{
    public class MainMenuViewModel : ViewModelBase<EmptyParameters>
    {
        public MainMenuViewModel(ITinyMessengerHub messengerHub) 
            : base(messengerHub)
        {
        }

        public ICommand FindBeersCommand
        {
            get { return new MvxRelayCommand(() => Navigate<BeerSearchViewModel, EmptyParameters>(null));}
        }

        public ICommand FindBreweriesCommand
        {
            get { return new MvxRelayCommand(() => Navigate<BrewerySearchViewModel, EmptyParameters>(null));}
        }

        public ICommand ViewFavoriteBeersCommand
        {
            get { return new MvxRelayCommand(() => Navigate<FavoriteBeersViewModel, EmptyParameters>(null)); }
        }

        public ICommand ViewFavoriteBreweriesCommand
        {
            get { return new MvxRelayCommand(() => Navigate<FavoriteBreweriesViewModel, EmptyParameters>(null)); }
        }

        protected override bool RequiresLoading
        {
            get { return false; }
        }
    }
}
