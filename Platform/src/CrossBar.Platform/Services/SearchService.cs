using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amarillo;
using Amarillo.Entities;
using CrossBar.Platform.Caching;
using TinyMessenger;

namespace CrossBar.Platform.Services
{
    public class SearchService : ServiceBase, ISearchService
    {
        private readonly IAmarillo _client;
        private readonly LruCache<int, Beer> _beerCache = new LruCache<int, Beer>(50);
        private readonly LruCache<int, Brewery> _breweryCache = new LruCache<int, Brewery>(50); 

        public SearchService(IAmarillo client, ITinyMessengerHub messengerHub)
            : base(messengerHub)
        {
            _client = client;
        }

        public Task<IList<Beer>> FindBeers(string query)
        {
            return MakeClientCall(() => _client.ListBeersAsync(query))
                .ContinueWith(response =>
                                  {
                                      if (response.Status == TaskStatus.RanToCompletion && response.Result != null)
                                      {
                                          foreach (var beer in response.Result.Beers)
                                          {
                                              _beerCache[beer.Id] = beer;

                                              if (beer.Brewery != null)
                                                _breweryCache[beer.Brewery.Id] = beer.Brewery;
                                          }

                                          return response.Result.Beers;
                                      }

                                      return null;
                                  });
        }

        public Task<Beer> GetBeer(int id)
        {
            var cachedBeer = _beerCache[id];

            if (cachedBeer != null)
                return Task.Factory.StartNew(() => cachedBeer);

            return MakeClientCall(() => _client.GetBeerAsync(id))
                .ContinueWith(response =>
                {
                    if (response.Status == TaskStatus.RanToCompletion && response.Result != null)
                    {
                        _beerCache[response.Result.Id] = response.Result;

                        if (response.Result.Brewery != null)
                            _breweryCache[response.Result.Brewery.Id] = response.Result.Brewery;

                        return response.Result;
                    }

                    return null;
                });
        }

        public Task<IList<Brewery>> FindBreweries(string query)
        {
            return MakeClientCall(() => _client.ListBreweriesAsync(query))
                .ContinueWith(response =>
                                  {
                                      if (response.Status == TaskStatus.RanToCompletion && response.Result != null)
                                      {
                                          foreach (var brewery in response.Result.Breweries)
                                          {
                                              _breweryCache[brewery.Id] = brewery;
                                          }

                                          return response.Result.Breweries;
                                      }

                                      return null;
                                  });
        }

        public Task<Brewery> GetBrewery(int id)
        {
            var cachedBrewery = _breweryCache[id];

            if (cachedBrewery != null)
                return Task.Factory.StartNew(() => cachedBrewery);

            return MakeClientCall(() => _client.GetBreweryAsync(id))
                .ContinueWith(response =>
                                  {
                                      if (response.Status == TaskStatus.RanToCompletion && response.Result != null)
                                      {
                                          _breweryCache[response.Result.Id] = response.Result;

                                          return response.Result;
                                      }

                                      return null;
                                  });
        }
    }
}
