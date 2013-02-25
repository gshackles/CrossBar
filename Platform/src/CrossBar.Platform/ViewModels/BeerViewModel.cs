using System;
using System.Windows.Input;
using Amarillo.Entities;
using Cirrious.MvvmCross.Commands;
using CrossBar.Platform.DataAccess.Entities;
using CrossBar.Platform.DataAccess.Repositories;
using CrossBar.Platform.Services;
using CrossBar.Platform.ViewModels.Parameters;
using TinyMessenger;

namespace CrossBar.Platform.ViewModels
{
    public class BeerViewModel : ViewModelBase<BeerParameters>
    {
        private readonly ISearchService _searchService;
        private readonly IFavoriteBeerRepository _favoriteBeerRepository;
        private FavoriteBeer _favorite;

        public BeerViewModel(ITinyMessengerHub messengerHub, ISearchService searchService, IFavoriteBeerRepository favoriteBeerRepository) 
            : base(messengerHub)
        {
            _searchService = searchService;
            _favoriteBeerRepository = favoriteBeerRepository;
        }

        private Beer _beer;
        public Beer Beer
        {
            get { return _beer; }
            set { _beer = value; RaisePropertyChanged(() => Beer); }
        }

        private bool _isFavorite;
        public bool IsFavorite
        {
            get { return _isFavorite; }
            set { _isFavorite = value; RaisePropertyChanged(() => IsFavorite); }
        }

        private bool _favoriteOperationInProgress;
        public bool FavoriteOperationInProgress
        {
            get { return _favoriteOperationInProgress; }
            set { _favoriteOperationInProgress = value; RaisePropertyChanged(() => FavoriteOperationInProgress); }
        }

        public ICommand ToggleFavoriteCommand
        {
            get { return new MvxRelayCommand(toggleFavorite);}
        }

        public ICommand SelectBreweryCommand
        {
            get
            {
                return new MvxRelayCommand(
                    () => Navigate<BreweryViewModel, BreweryParameters>(new BreweryParameters(Beer.Brewery.Id)));
            }
        }

        protected internal override void Initialize(BeerParameters parameters)
        {
            _searchService
                .GetBeer(parameters.BeerId)
                .ContinueWith(response =>
                                  {
                                      Beer = response.Result;

                                      FinishedLoading();

                                      FavoriteOperationInProgress = true;

                                      _favoriteBeerRepository
                                          .CheckForFavorite(Beer.Id)
                                          .ContinueWith(faveResponse =>
                                                            {
                                                                FavoriteOperationInProgress = false;

                                                                if (faveResponse.Result == null) return;

                                                                _favorite = faveResponse.Result;
                                                                IsFavorite = true;
                                                            });
                                  });
        }

        private void toggleFavorite()
        {
            FavoriteOperationInProgress = true;

            if (IsFavorite)
            {
                _favoriteBeerRepository
                    .RemoveFavorite(_favorite)
                    .ContinueWith(response =>
                                      {
                                          _favorite = null;
                                          IsFavorite = false;
                                          FavoriteOperationInProgress = false;
                                      });
            }
            else
            {
                _favoriteBeerRepository
                    .SaveFavorite(Beer.Id, Beer.Name)
                    .ContinueWith(response =>
                                      {
                                          _favorite = response.Result;
                                          IsFavorite = true;
                                          FavoriteOperationInProgress = false;
                                      });
            }
        }
    }
}