using Amarillo.Entities;
using CrossBar.Platform.Services;
using CrossBar.Platform.ViewModels.Parameters;
using TinyMessenger;

namespace CrossBar.Platform.ViewModels
{
    public class BreweryViewModel : ViewModelBase<BreweryParameters>
    {
        private readonly ISearchService _searchService;

        public BreweryViewModel(ITinyMessengerHub messengerHub, ISearchService searchService)
            : base(messengerHub)
        {
            _searchService = searchService;
        }

        private Brewery _brewery;
        public Brewery Brewery
        {
            get { return _brewery; }
            set { _brewery = value; RaisePropertyChanged(() => Brewery); }
        }

        protected internal override void Initialize(BreweryParameters parameters)
        {
            _searchService
                .GetBrewery(parameters.BreweryId)
                .ContinueWith(response =>
                                  {
                                      Brewery = response.Result;

                                      FinishedLoading();
                                  });
        }
    }
}