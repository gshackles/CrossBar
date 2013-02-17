using System.Collections.Generic;
using System.Windows.Input;
using Cirrious.MvvmCross.Commands;
using CrossBar.Platform.DataAccess.Entities;
using CrossBar.Platform.DataAccess.Repositories;
using CrossBar.Platform.ViewModels.Parameters;
using TinyMessenger;

namespace CrossBar.Platform.ViewModels
{
    public class FavoriteBeersViewModel : ViewModelBase<EmptyParameters>
    {
        private readonly IFavoriteBeerRepository _favoriteBeerRepository;

        public FavoriteBeersViewModel(ITinyMessengerHub messengerHub, IFavoriteBeerRepository favoriteBeerRepository) 
            : base(messengerHub)
        {
            _favoriteBeerRepository = favoriteBeerRepository;
        }

        private IList<FavoriteBeer> _favoriteBeers;
        public IList<FavoriteBeer> FavoriteBeers
        {
            get { return _favoriteBeers; }
            set { _favoriteBeers = value; RaisePropertyChanged(() => FavoriteBeers); }
        }

        public ICommand SelectFavoriteCommand
        {
            get
            {
                return
                    new MvxRelayCommand<FavoriteBeer>(
                        fave => Navigate<BeerViewModel, BeerParameters>(new BeerParameters(fave.BeerId)));
            }
        }

        protected internal override void Initialize(EmptyParameters parameters)
        {
            _favoriteBeerRepository
                .ListFavoriteBeers()
                .ContinueWith(result =>
                                  {
                                      FavoriteBeers = result.Result;

                                      FinishedLoading();
                                  });
        }
    }
}