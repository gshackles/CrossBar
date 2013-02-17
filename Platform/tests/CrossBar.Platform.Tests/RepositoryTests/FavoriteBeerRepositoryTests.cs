using System.Linq;
using Amarillo.Entities;
using CrossBar.Platform.DataAccess.Repositories;
using CrossBar.Platform.Tests.Extensions;
using NUnit.Framework;

namespace CrossBar.Platform.Tests.RepositoryTests
{
    [TestFixture]
    public class FavoriteBeerRepositoryTests
    {
        private IFavoriteBeerRepository _favoriteBeerRepository;
        private TemporarySQLiteConnectionFactory _connectionFactory;

        [SetUp]
        public void SetUp()
        {
            _connectionFactory = new TemporarySQLiteConnectionFactory();
            _favoriteBeerRepository = new FavoriteBeerRepository(_connectionFactory);
        }

        [TearDown]
        public void TearDown()
        {
            _connectionFactory.CleanUp();
        }

        [Test]
        public void SaveFavorite_SavesToDatabase_ReturnsSavedFavorite()
        {
            var beer = new Beer {Id = 42, Name = "Duff Dark"};

            var fave = _favoriteBeerRepository.SaveFavorite(beer.Id, beer.Name).Test();

            Assert.AreNotEqual(0, fave.Id);
            Assert.AreEqual(beer.Id, fave.BeerId);
            Assert.AreEqual(beer.Name, fave.Name);

            var allFaves = _favoriteBeerRepository.ListFavoriteBeers().Test();

            Assert.AreEqual(1, allFaves.Count());

            var savedFave = allFaves.First();

            Assert.AreEqual(fave.Id, savedFave.Id);
            Assert.AreEqual(fave.BeerId, savedFave.BeerId);
            Assert.AreEqual(fave.Name, savedFave.Name);
        }

        [Test]
        public void ListFavoriteBeers_NothingSaved_ReturnsEmptyList()
        {
            var allFaves = _favoriteBeerRepository.ListFavoriteBeers().Test();

            Assert.AreEqual(0, allFaves.Count());
        }

        [Test]
        public void ListFavoriteBeers_BeersSaved_ReturnsAllFavorites()
        {
            int numFaves = 5;

            for (int i = 0; i < numFaves; i++)
                _favoriteBeerRepository.SaveFavorite(42, "Duff Dark").Test();

            var allFaves = _favoriteBeerRepository.ListFavoriteBeers().Test();

            Assert.AreEqual(numFaves, allFaves.Count());
        }

        [Test]
        public void CheckForFavorite_BeerNotSavedAsFavorite_ReturnsNull()
        {
            var beer = new Beer { Id = 42, Name = "Duff Dark" };

            var fave = _favoriteBeerRepository.CheckForFavorite(beer.Id).Test();

            Assert.IsNull(fave);
        }

        [Test]
        public void CheckForFavorite_BeerSavedAsFavorite_ReturnsFavorite()
        {
            var beer = new Beer { Id = 42, Name = "Duff Dark" };

            var fave = _favoriteBeerRepository.SaveFavorite(beer.Id, beer.Name).Test();

            Assert.AreNotEqual(0, fave.Id);
            Assert.AreEqual(beer.Id, fave.BeerId);
            Assert.AreEqual(beer.Name, fave.Name);

            var checkedFavorite = _favoriteBeerRepository.CheckForFavorite(beer.Id).Test();

            Assert.AreEqual(fave.Id, checkedFavorite.Id);
            Assert.AreEqual(fave.BeerId, checkedFavorite.BeerId);
            Assert.AreEqual(fave.Name, checkedFavorite.Name);
        }

        [Test]
        public void RemoveFavorite_FavoriteAlreadyRemoved_DoesNothing()
        {
            var beer = new Beer { Id = 42, Name = "Duff Dark" };
            
            var fave = _favoriteBeerRepository.SaveFavorite(beer.Id, beer.Name).Test();

            var allFaves = _favoriteBeerRepository.ListFavoriteBeers().Test();

            Assert.AreEqual(1, allFaves.Count);

            _favoriteBeerRepository.RemoveFavorite(fave).Wait();

            allFaves = _favoriteBeerRepository.ListFavoriteBeers().Test();

            Assert.AreEqual(0, allFaves.Count);

            _favoriteBeerRepository.RemoveFavorite(fave).Wait();

            allFaves = _favoriteBeerRepository.ListFavoriteBeers().Test();

            Assert.AreEqual(0, allFaves.Count);
        }

        [Test]
        public void RemoveFavorite_FavoriteExists_RemovesFavorite()
        {
            var beer = new Beer { Id = 42, Name = "Duff Dark" };

            var fave = _favoriteBeerRepository.SaveFavorite(beer.Id, beer.Name).Test();

            var allFaves = _favoriteBeerRepository.ListFavoriteBeers().Test();

            Assert.AreEqual(1, allFaves.Count);

            _favoriteBeerRepository.RemoveFavorite(fave).Wait();

            allFaves = _favoriteBeerRepository.ListFavoriteBeers().Test();

            Assert.AreEqual(0, allFaves.Count);
        }
    }
}
