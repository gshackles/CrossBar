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
    public class BreweryViewModel : ViewModelBase<BreweryParameters>
    {
        private readonly ISearchService _searchService;
        private readonly IFavoriteBreweryRepository _favoriteBreweryRepository;
        private FavoriteBrewery _favorite;

        public BreweryViewModel(ITinyMessengerHub messengerHub, ISearchService searchService, IFavoriteBreweryRepository favoriteBreweryRepository)
            : base(messengerHub)
        {
            _searchService = searchService;
            _favoriteBreweryRepository = favoriteBreweryRepository;
        }

        private Brewery _brewery;
        public Brewery Brewery
        {
            get { return _brewery; }
            set { _brewery = value; RaisePropertyChanged(() => Brewery); }
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
            get { return new MvxRelayCommand(toggleFavorite); }
        }

        protected internal override void Initialize(BreweryParameters parameters)
        {
            _searchService
                .GetBrewery(parameters.BreweryId)
                .ContinueWith(response =>
                                  {
                                      Brewery = response.Result;

                                      FinishedLoading();

                                      FavoriteOperationInProgress = true;

                                      _favoriteBreweryRepository
                                          .CheckForFavorite(Brewery.Id)
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
                _favoriteBreweryRepository
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
                _favoriteBreweryRepository
                    .SaveFavorite(Brewery.Id, Brewery.Name)
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