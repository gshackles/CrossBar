using System.Linq;
using Amarillo.Entities;
using CrossBar.Platform.DataAccess.Repositories;
using CrossBar.Platform.Tests.Extensions;
using NUnit.Framework;

namespace CrossBar.Platform.Tests.RepositoryTests
{
    [TestFixture]
    public class FavoriteBreweryRepositoryTests
    {
        private IFavoriteBreweryRepository _favoriteBreweryRepository;
        private TemporarySQLiteConnectionFactory _connectionFactory;

        [SetUp]
        public void SetUp()
        {
            _connectionFactory = new TemporarySQLiteConnectionFactory();
            _favoriteBreweryRepository = new FavoriteBreweryRepository(_connectionFactory);
        }

        [TearDown]
        public void TearDown()
        {
            _connectionFactory.CleanUp();
        }

        [Test]
        public void SaveFavorite_SavesToDatabase_ReturnsSavedFavorite()
        {
            var brewery = new Brewery { Id = 42, Name = "Duff" };

            var fave = _favoriteBreweryRepository.SaveFavorite(brewery.Id, brewery.Name).Test();

            Assert.AreNotEqual(0, fave.Id);
            Assert.AreEqual(brewery.Id, fave.BreweryId);
            Assert.AreEqual(brewery.Name, fave.Name);

            var allFaves = _favoriteBreweryRepository.ListFavoriteBreweries().Test();

            Assert.AreEqual(1, allFaves.Count());

            var savedFave = allFaves.First();

            Assert.AreEqual(fave.Id, savedFave.Id);
            Assert.AreEqual(fave.BreweryId, savedFave.BreweryId);
            Assert.AreEqual(fave.Name, savedFave.Name);
        }

        [Test]
        public void ListFavoriteBreweries_NothingSaved_ReturnsEmptyList()
        {
            var allFaves = _favoriteBreweryRepository.ListFavoriteBreweries().Test();

            Assert.AreEqual(0, allFaves.Count());
        }

        [Test]
        public void ListFavoriteBreweries_BreweriesSaved_ReturnsAllFavorites()
        {
            int numFaves = 5;

            for (int i = 0; i < numFaves; i++)
                _favoriteBreweryRepository.SaveFavorite(42, "Duff").Test();

            var allFaves = _favoriteBreweryRepository.ListFavoriteBreweries().Test();

            Assert.AreEqual(numFaves, allFaves.Count());
        }

        [Test]
        public void CheckForFavorite_BreweryNotSavedAsFavorite_ReturnsNull()
        {
            var brewery = new Brewery { Id = 42, Name = "Duff" };

            var fave = _favoriteBreweryRepository.CheckForFavorite(brewery.Id).Test();

            Assert.IsNull(fave);
        }

        [Test]
        public void CheckForFavorite_BrewerySavedAsFavorite_ReturnsFavorite()
        {
            var brewery = new Brewery { Id = 42, Name = "Duff" };

            var fave = _favoriteBreweryRepository.SaveFavorite(brewery.Id, brewery.Name).Test();

            Assert.AreNotEqual(0, fave.Id);
            Assert.AreEqual(brewery.Id, fave.BreweryId);
            Assert.AreEqual(brewery.Name, fave.Name);

            var checkedFavorite = _favoriteBreweryRepository.CheckForFavorite(brewery.Id).Test();

            Assert.AreEqual(fave.Id, checkedFavorite.Id);
            Assert.AreEqual(fave.BreweryId, checkedFavorite.BreweryId);
            Assert.AreEqual(fave.Name, checkedFavorite.Name);
        }

        [Test]
        public void RemoveFavorite_FavoriteAlreadyRemoved_DoesNothing()
        {
            var brewery = new Brewery { Id = 42, Name = "Duff" };

            var fave = _favoriteBreweryRepository.SaveFavorite(brewery.Id, brewery.Name).Test();

            var allFaves = _favoriteBreweryRepository.ListFavoriteBreweries().Test();

            Assert.AreEqual(1, allFaves.Count);

            _favoriteBreweryRepository.RemoveFavorite(fave).Wait();

            allFaves = _favoriteBreweryRepository.ListFavoriteBreweries().Test();

            Assert.AreEqual(0, allFaves.Count);

            _favoriteBreweryRepository.RemoveFavorite(fave).Wait();

            allFaves = _favoriteBreweryRepository.ListFavoriteBreweries().Test();

            Assert.AreEqual(0, allFaves.Count);
        }

        [Test]
        public void RemoveFavorite_FavoriteExists_RemovesFavorite()
        {
            var brewery = new Brewery { Id = 42, Name = "Duff" };

            var fave = _favoriteBreweryRepository.SaveFavorite(brewery.Id, brewery.Name).Test();

            var allFaves = _favoriteBreweryRepository.ListFavoriteBreweries().Test();

            Assert.AreEqual(1, allFaves.Count);

            _favoriteBreweryRepository.RemoveFavorite(fave).Wait();

            allFaves = _favoriteBreweryRepository.ListFavoriteBreweries().Test();

            Assert.AreEqual(0, allFaves.Count);
        }
    }
}