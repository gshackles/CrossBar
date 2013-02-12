using System;
using System.Collections.Generic;
using System.Windows.Input;
using Amarillo.Entities;
using Cirrious.MvvmCross.Commands;
using CrossBar.Platform.Services;
using CrossBar.Platform.ViewModels.Parameters;
using TinyMessenger;

namespace CrossBar.Platform.ViewModels
{
    public class BrewerySearchViewModel : ViewModelBase<EmptyParameters>
    {
        private readonly ISearchService _searchService;

        public BrewerySearchViewModel(ITinyMessengerHub messengerHub, ISearchService searchService) 
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

        private IEnumerable<Brewery> _breweries;
        public IEnumerable<Brewery> Breweries
        {
            get { return _breweries; }
            set { _breweries = value; RaisePropertyChanged(() => Breweries); }
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

        public ICommand SelectBreweryCommand
        {
            get { return new MvxRelayCommand<Brewery>(selectBrewery);}
        }

        private void search()
        {
            if (IsSearching || string.IsNullOrWhiteSpace(Query)) 
                return;

            IsSearching = true;

            _searchService
                .FindBreweries(Query)
                .ContinueWith(response =>
                                  {
                                      IsSearching = false;
                                      Breweries = response.Result;
                                  });
        }

        private void selectBrewery(Brewery brewery)
        {
            Navigate<BreweryViewModel, BreweryParameters>(new BreweryParameters(brewery.Id));
        }
    }
}