using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amarillo.Entities;
using CrossBar.Platform.DataAccess.Entities;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace CrossBar.Platform.DataAccess.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ISQLiteConnectionFactory _connectionFactory;
        private const string DatabaseName = "Favorites.db";

        public FavoriteRepository(ISQLiteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;

            using (var conn = _connectionFactory.Create(DatabaseName))
            {
                conn.CreateTable<FavoriteBeer>();
            }
        }

        public Task<List<FavoriteBeer>> ListFavoriteBeers()
        {
            return Task.Factory.StartNew(() =>
            {
                using (var conn = _connectionFactory.Create(DatabaseName))
                {
                    return conn.Table<FavoriteBeer>().OrderBy(beer => beer.Name).ToList();
                }
            });
        }

        public Task<FavoriteBeer> CheckForFavorite(Beer beer)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var conn = _connectionFactory.Create(DatabaseName))
                {
                    return conn.Table<FavoriteBeer>().SingleOrDefault(favorite => favorite.BeerId == beer.Id);
                }
            });
        }

        public Task<FavoriteBeer> SaveFavorite(Beer beer)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var conn = _connectionFactory.Create(DatabaseName))
                {
                    var favorite = new FavoriteBeer
                                       {
                                           BeerId = beer.Id,
                                           Name = beer.Name
                                       };

                    conn.Insert(favorite);

                    return favorite;
                }
            });
        }

        public Task RemoveFavorite(FavoriteBeer favorite)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var conn = _connectionFactory.Create(DatabaseName))
                {
                    conn.Delete(favorite);
                }
            });
        }
    }
}
