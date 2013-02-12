using System.Collections.Generic;
using System.Threading.Tasks;
using Amarillo.Entities;
using CrossBar.Platform.DataAccess.Entities;

namespace CrossBar.Platform.DataAccess.Repositories
{
    public interface IFavoriteRepository
    {
        Task<List<FavoriteBeer>> ListFavoriteBeers();
        Task<FavoriteBeer> CheckForFavorite(Beer beer);
        Task<FavoriteBeer> SaveFavorite(Beer beer);
        Task RemoveFavorite(FavoriteBeer favorite);
    }
}