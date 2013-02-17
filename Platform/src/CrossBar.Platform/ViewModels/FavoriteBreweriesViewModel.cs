using System.Collections.Generic;
using System.Windows.Input;
using Cirrious.MvvmCross.Commands;
using CrossBar.Platform.DataAccess.Entities;
using CrossBar.Platform.DataAccess.Repositories;
using CrossBar.Platform.ViewModels.Parameters;
using TinyMessenger;

namespace CrossBar.Platform.ViewModels
{
    public class FavoriteBreweriesViewModel : ViewModelBase<EmptyParameters>
    {
        private readonly IFavoriteBreweryRepository _favoriteBreweryRepository;

        public FavoriteBreweriesViewModel(ITinyMessengerHub messengerHub, IFavoriteBreweryRepository favoriteBreweryRepository)
            : base(messengerHub)
        {
            _favoriteBreweryRepository = favoriteBreweryRepository;
        }

        private IList<FavoriteBrewery> _favoriteBreweries;
        public IList<FavoriteBrewery> FavoriteBreweries
        {
            get { return _favoriteBreweries; }
            set { _favoriteBreweries = value; RaisePropertyChanged(() => FavoriteBreweries); }
        }

        public ICommand SelectFavoriteCommand
        {
            get
            {
                return
                    new MvxRelayCommand<FavoriteBrewery>(
                        fave => Navigate<BreweryViewModel, BreweryParameters>(new BreweryParameters(fave.BreweryId)));
            }
        }

        protected internal override void Initialize(EmptyParameters parameters)
        {
            _favoriteBreweryRepository
                .ListFavoriteBreweries()
                .ContinueWith(result =>
                                  {
                                      FavoriteBreweries = result.Result;

                                      FinishedLoading();
                                  });
        }
    }
}