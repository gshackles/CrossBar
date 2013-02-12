﻿using System.Linq;
using Amarillo.Entities;
using CrossBar.Platform.DataAccess.Repositories;
using CrossBar.Platform.Tests.Extensions;
using NUnit.Framework;

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
        public void SaveFavorite_SavesToDatabase_ReturnsSavedFavorite()
        {
            var beer = new Beer {Id = 42, Name = "Duff Dark"};

            var fave = _favoriteRepository.SaveFavorite(beer).Test();

            Assert.AreNotEqual(0, fave.Id);
            Assert.AreEqual(beer.Id, fave.BeerId);
            Assert.AreEqual(beer.Name, fave.Name);

            var allFaves = _favoriteRepository.ListFavoriteBeers().Test();

            Assert.AreEqual(1, allFaves.Count());

            var savedFave = allFaves.First();

            Assert.AreEqual(fave.Id, savedFave.Id);
            Assert.AreEqual(fave.BeerId, savedFave.BeerId);
            Assert.AreEqual(fave.Name, savedFave.Name);
        }

        [Test]
        public void ListFavoriteBeers_NothingSaved_ReturnsEmptyList()
        {
            var allFaves = _favoriteRepository.ListFavoriteBeers().Test();

            Assert.AreEqual(0, allFaves.Count());
        }

        [Test]
        public void ListFavoriteBeers_BeersSaved_ReturnsAllFavorites()
        {
            int numFaves = 5;

            for (int i = 0; i < numFaves; i++)
                _favoriteRepository.SaveFavorite(new Beer { Id = 42, Name = "Duff Dark" }).Test();

            var allFaves = _favoriteRepository.ListFavoriteBeers().Test();

            Assert.AreEqual(numFaves, allFaves.Count());
        }

        [Test]
        public void CheckForFavorite_BeerNotSavedAsFavorite_ReturnsNull()
        {
            var beer = new Beer { Id = 42, Name = "Duff Dark" };

            var fave = _favoriteRepository.CheckForFavorite(beer).Test();

            Assert.IsNull(fave);
        }

        [Test]
        public void CheckForFavorite_BeerSavedAsFavorite_ReturnsFavorite()
        {
            var beer = new Beer { Id = 42, Name = "Duff Dark" };

            var fave = _favoriteRepository.SaveFavorite(beer).Test();

            Assert.AreNotEqual(0, fave.Id);
            Assert.AreEqual(beer.Id, fave.BeerId);
            Assert.AreEqual(beer.Name, fave.Name);

            var checkedFavorite = _favoriteRepository.CheckForFavorite(beer).Test();

            Assert.AreEqual(fave.Id, checkedFavorite.Id);
            Assert.AreEqual(fave.BeerId, checkedFavorite.BeerId);
            Assert.AreEqual(fave.Name, checkedFavorite.Name);
        }

        [Test]
        public void RemoveFavorite_FavoriteAlreadyRemoved_DoesNothing()
        {
            var beer = new Beer { Id = 42, Name = "Duff Dark" };
            
            var fave = _favoriteRepository.SaveFavorite(beer).Test();

            var allFaves = _favoriteRepository.ListFavoriteBeers().Test();

            Assert.AreEqual(1, allFaves.Count);

            _favoriteRepository.RemoveFavorite(fave).Wait();

            allFaves = _favoriteRepository.ListFavoriteBeers().Test();

            Assert.AreEqual(0, allFaves.Count);

            _favoriteRepository.RemoveFavorite(fave).Wait();

            allFaves = _favoriteRepository.ListFavoriteBeers().Test();

            Assert.AreEqual(0, allFaves.Count);
        }

        [Test]
        public void RemoveFavorite_FavoriteExists_RemovesFavorite()
        {
            var beer = new Beer { Id = 42, Name = "Duff Dark" };

            var fave = _favoriteRepository.SaveFavorite(beer).Test();

            var allFaves = _favoriteRepository.ListFavoriteBeers().Test();

            Assert.AreEqual(1, allFaves.Count);

            _favoriteRepository.RemoveFavorite(fave).Wait();

            allFaves = _favoriteRepository.ListFavoriteBeers().Test();

            Assert.AreEqual(0, allFaves.Count);
        }
    }
}
