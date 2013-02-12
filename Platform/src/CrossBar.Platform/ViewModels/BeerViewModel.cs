using Amarillo.Entities;
using CrossBar.Platform.Services;
using CrossBar.Platform.ViewModels.Parameters;
using TinyMessenger;

namespace CrossBar.Platform.ViewModels
{
    public class BeerViewModel : ViewModelBase<BeerParameters>
    {
        private readonly ISearchService _searchService;

        public BeerViewModel(ITinyMessengerHub messengerHub, ISearchService searchService) 
            : base(messengerHub)
        {
            _searchService = searchService;
        }

        private Beer _beer;
        public Beer Beer
        {
            get { return _beer; }
            set { _beer = value; RaisePropertyChanged(() => Beer); }
        }

        protected internal override void Initialize(BeerParameters parameters)
        {
            _searchService
                .GetBeer(parameters.BeerId)
                .ContinueWith(response =>
                                  {
                                      Beer = response.Result;

                                      FinishedLoading();
                                  });
        }
    }
}