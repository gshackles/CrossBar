using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossBar.Platform.DataAccess.Entities;
using CrossBar.Platform.DataAccess.Repositories;

namespace CrossBar.Platform.Tests.Mocks
{
    public class InMemoryFavoriteBreweryRepository : IFavoriteBreweryRepository
    {
        private readonly List<FavoriteBrewery> _favoriteBreweries = new List<FavoriteBrewery>();
        private int _lastId = 0;

        public Task<List<FavoriteBrewery>> ListFavoriteBreweries()
        {
            return Task.Factory.StartNew(() => _favoriteBreweries);
        }

        public Task<FavoriteBrewery> CheckForFavorite(int breweryId)
        {
            return Task.Factory.StartNew(() => _favoriteBreweries.FirstOrDefault(fave => fave.BreweryId == breweryId));
        }

        public Task<FavoriteBrewery> SaveFavorite(int breweryId, string breweryName)
        {
            return Task.Factory.StartNew(() =>
                                             {
                                                 var fave = new FavoriteBrewery
                                                                {
                                                                    Id = ++_lastId,
                                                                    Name = breweryName,
                                                                    BreweryId = breweryId
                                                                };

                                                 _favoriteBreweries.Add(fave);

                                                 return fave;
                                             });
        }

        public Task RemoveFavorite(FavoriteBrewery favorite)
        {
            return Task.Factory.StartNew(() => _favoriteBreweries.Remove(favorite));
        }
    }
}