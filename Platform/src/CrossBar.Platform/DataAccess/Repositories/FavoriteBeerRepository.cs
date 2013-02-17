using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossBar.Platform.DataAccess.Entities;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace CrossBar.Platform.DataAccess.Repositories
{
    public class FavoriteBeerRepository : IFavoriteBeerRepository
    {
        private readonly ISQLiteConnectionFactory _connectionFactory;
        private const string DatabaseName = "FavoriteBeers.db";

        public FavoriteBeerRepository(ISQLiteConnectionFactory connectionFactory)
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

        public Task<FavoriteBeer> CheckForFavorite(int beerId)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var conn = _connectionFactory.Create(DatabaseName))
                {
                    return conn.Table<FavoriteBeer>().SingleOrDefault(favorite => favorite.BeerId == beerId);
                }
            });
        }

        public Task<FavoriteBeer> SaveFavorite(int beerId, string beerName)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var conn = _connectionFactory.Create(DatabaseName))
                {
                    var favorite = new FavoriteBeer
                                       {
                                           BeerId = beerId,
                                           Name = beerName
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
