using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CrossBar.Platform.DataAccess.Repositories;
using NUnit.Framework;
using SQLite;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace CrossBar.Platform.Tests.RepositoryTests
{
    [TestFixture]
    public class FavoriteRepositoryTests
    {
        private IFavoriteRepository _favoriteRepository;
        private TemporarySQLiteConnectionFactory _connectionFactory;

        [SetUp]
        public void SetUp()
        {
            _connectionFactory = new TemporarySQLiteConnectionFactory();
            _favoriteRepository = new FavoriteRepository(_connectionFactory);
        }

        [TearDown]
        public void TearDown()
        {
            _connectionFactory.CleanUp();
        }

        [Test]
        public void Foo()
        {
        }

        public class TemporarySQLiteConnectionFactory : ISQLiteConnectionFactory
	    {
            private string _filePath;

		    public ISQLiteConnection Create(string address)
		    {
			    _filePath = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid().ToString(), address);

			    return new SQLiteConnection(_filePath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
		    }

            public void CleanUp()
            {
                File.Delete(_filePath);
            }
	    }
    }
}
