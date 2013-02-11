using System.Collections.Generic;
using System.Windows.Input;
using Amarillo.Entities;
using Cirrious.MvvmCross.Commands;
using CrossBar.Platform.Services;
using CrossBar.Platform.ViewModels.Parameters;
using TinyMessenger;

namespace CrossBar.Platform.ViewModels
{
    public class BeerSearchViewModel : ViewModelBase<EmptyParameters>
    {
        private readonly ISearchService _searchService;

        public BeerSearchViewModel(ITinyMessengerHub messengerHub, ISearchService searchService) 
            : base(messengerHub)
        {
            _searchService = searchService;
        }

        protected override bool RequiresLoading
        {
            get { return false; }
        }

        private bool _isSearching;
        public bool IsSearching
        {
            get { return _isSearching; }
            set { _isSearching = value; RaisePropertyChanged(() => IsSearching); }
        }

        private IEnumerable<Beer> _beers;
        public IEnumerable<Beer> Beers
        {
            get { return _beers; }
            set { _beers = value; RaisePropertyChanged(() => Beers); }
        }

        private string _query;
        public string Query
        {
            get { return _query; }
            set { _query = value; RaisePropertyChanged(() => Query); }
        }

        public ICommand SearchCommand
        {
            get { return new MvxRelayCommand(search);}
        }

        public ICommand SelectBeerCommand
        {
            get { return new MvxRelayCommand<Beer>(selectBeer);}
        }

        private void search()
        {
            if (IsSearching || string.IsNullOrWhiteSpace(Query)) 
                return;

            IsSearching = true;

            _searchService
                .FindBeers(Query)
                .ContinueWith(response =>
                                  {
                                      IsSearching = false;
                                      Beers = response.Result;
                                  });
        }

        private void selectBeer(Beer beer)
        {
            Navigate<BeerViewModel, BeerParameters>(new BeerParameters(beer.Id));
        }
    }
}