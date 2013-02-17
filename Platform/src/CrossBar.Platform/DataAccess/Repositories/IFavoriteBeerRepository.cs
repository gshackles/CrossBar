using System.Collections.Generic;
using System.Threading.Tasks;
using CrossBar.Platform.DataAccess.Entities;

namespace CrossBar.Platform.DataAccess.Repositories
{
    public interface IFavoriteBeerRepository
    {
        Task<List<FavoriteBeer>> ListFavoriteBeers();
        Task<FavoriteBeer> CheckForFavorite(int beerId);
        Task<FavoriteBeer> SaveFavorite(int beerId, string beerName);
        Task RemoveFavorite(FavoriteBeer favorite);
    }
}