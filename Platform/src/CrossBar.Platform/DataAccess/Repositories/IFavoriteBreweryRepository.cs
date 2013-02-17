using System.Collections.Generic;
using System.Threading.Tasks;
using CrossBar.Platform.DataAccess.Entities;

namespace CrossBar.Platform.DataAccess.Repositories
{
    public interface IFavoriteBreweryRepository
    {
        Task<List<FavoriteBrewery>> ListFavoriteBreweries();
        Task<FavoriteBrewery> CheckForFavorite(int breweryId);
        Task<FavoriteBrewery> SaveFavorite(int breweryId, string breweryName);
        Task RemoveFavorite(FavoriteBrewery favorite);
    }
}