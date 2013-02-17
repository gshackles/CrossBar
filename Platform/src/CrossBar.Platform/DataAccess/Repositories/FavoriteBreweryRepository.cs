using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Sqlite;
using CrossBar.Platform.DataAccess.Entities;

namespace CrossBar.Platform.DataAccess.Repositories
{
    public class FavoriteBreweryRepository : IFavoriteBreweryRepository
    {
        private readonly ISQLiteConnectionFactory _connectionFactory;
        private const string DatabaseName = "FavoriteBreweries.db";

        public FavoriteBreweryRepository(ISQLiteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;

            using (var conn = _connectionFactory.Create(DatabaseName))
            {
                conn.CreateTable<FavoriteBrewery>();
            }
        }

        public Task<List<FavoriteBrewery>> ListFavoriteBreweries()
        {
            return Task.Factory.StartNew(() =>
            {
                using (var conn = _connectionFactory.Create(DatabaseName))
                {
                    return conn.Table<FavoriteBrewery>().OrderBy(brewery => brewery.Name).ToList();
                }
            });
        }

        public Task<FavoriteBrewery> CheckForFavorite(int breweryId)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var conn = _connectionFactory.Create(DatabaseName))
                {
                    return conn.Table<FavoriteBrewery>().SingleOrDefault(favorite => favorite.BreweryId == breweryId);
                }
            });
        }

        public Task<FavoriteBrewery> SaveFavorite(int breweryId, string breweryName)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var conn = _connectionFactory.Create(DatabaseName))
                {
                    var favorite = new FavoriteBrewery
                    {
                        BreweryId = breweryId,
                        Name = breweryName
                    };

                    conn.Insert(favorite);

                    return favorite;
                }
            });
        }

        public Task RemoveFavorite(FavoriteBrewery favorite)
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