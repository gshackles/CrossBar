using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amarillo.Entities;
using CrossBar.Platform.DataAccess.Entities;
using CrossBar.Platform.DataAccess.Repositories;

namespace CrossBar.Platform.Tests.Mocks
{
    public class InMemoryFavoriteRepository : IFavoriteRepository
    {
        private readonly List<FavoriteBeer> _favoriteBeers = new List<FavoriteBeer>();
        private int _lastId = 0;

        public Task<List<FavoriteBeer>> ListFavoriteBeers()
        {
            return Task.Factory.StartNew(() => _favoriteBeers);
        }

        public Task<FavoriteBeer> CheckForFavorite(Beer beer)
        {
            return Task.Factory.StartNew(() => _favoriteBeers.FirstOrDefault(fave => fave.BeerId == beer.Id));
        }

        public Task<FavoriteBeer> SaveFavorite(Beer beer)
        {
            return Task.Factory.StartNew(() =>
                                             {
                                                 var fave = new FavoriteBeer
                                                                {
                                                                    Id = ++_lastId,
                                                                    Name = beer.Name,
                                                                    BeerId = beer.Id
                                                                };

                                                 _favoriteBeers.Add(fave);

                                                 return fave;
                                             });
        }

        public Task RemoveFavorite(FavoriteBeer favorite)
        {
            return Task.Factory.StartNew(() => _favoriteBeers.Remove(favorite));
        }
    }
}
