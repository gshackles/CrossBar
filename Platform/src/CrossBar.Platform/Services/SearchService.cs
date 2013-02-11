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
                                              _beerCache[beer.Id] = beer;

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

                        return response.Result;
                    }

                    return null;
                });
        }
    }
}
