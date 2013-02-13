using System.Linq;
using System.Net;
using Amarillo.Entities;
using Amarillo.Network;
using CrossBar.Platform.Tests.Extensions;
using CrossBar.Platform.ViewModels;
using CrossBar.Platform.ViewModels.Parameters;
using NUnit.Framework;

namespace CrossBar.Platform.Tests.ViewModelTests
{
    public class BeerViewModelTests : ViewModelTestsBase<BeerViewModel, BeerParameters>
    {
        [Test]
        public void ViewModelLoaded_BeerIsFavorite_MarkedAsFavorite()
        {
            var beer = getTestBeer();
            FavoriteRepository.SaveFavorite(beer);

            Assert.AreEqual(1, FavoriteRepository.ListFavoriteBeers().Test().Count);

            var viewModel = getTestViewModel();

            Assert.IsTrue(viewModel.IsFavorite);
        }

        [Test]
        public void ViewModelLoaded_BeerIsNotFavorite_NotMarkedAsFavorite()
        {
            var viewModel = getTestViewModel();

            Assert.AreEqual(0, FavoriteRepository.ListFavoriteBeers().Test().Count);
            Assert.IsFalse(viewModel.IsFavorite);
        }

        [Test]
        public void ToggleFavoriteCommand_BeerIsFavorite_FavoriteIsRemoved()
        {
            var beer = getTestBeer();
            FavoriteRepository.SaveFavorite(beer);

            Assert.AreEqual(1, FavoriteRepository.ListFavoriteBeers().Test().Count);

            var viewModel = getTestViewModel();

            Assert.IsTrue(viewModel.IsFavorite);

            viewModel.ToggleFavoriteCommand.Execute(null);
            viewModel.FavoriteOperationInProgress.AllowToComplete();

            Assert.IsFalse(viewModel.IsFavorite);
            Assert.AreEqual(0, FavoriteRepository.ListFavoriteBeers().Test().Count);
        }

        [Test]
        public void ToggleFavoriteCommand_BeerIsNotFavorite_SavesFavorite()
        {
            var viewModel = getTestViewModel();

            Assert.IsFalse(viewModel.IsFavorite);
            Assert.AreEqual(0, FavoriteRepository.ListFavoriteBeers().Test().Count);

            viewModel.ToggleFavoriteCommand.Execute(null);
            viewModel.FavoriteOperationInProgress.AllowToComplete();

            Assert.IsTrue(viewModel.IsFavorite);
            Assert.AreEqual(1, FavoriteRepository.ListFavoriteBeers().Test().Count);
        }

        [Test]
        public void SelectBreweryCommand_NavigatesToBrewery()
        {
            var viewModel = getTestViewModel();

            viewModel.SelectBreweryCommand.Execute(null);

            Assert.AreEqual(1, Dispatcher.NavigateRequests.Count);
            Assert.That(Dispatcher.NavigateRequests.First().ViewModelType == typeof(BreweryViewModel));
        }

        private BeerViewModel getTestViewModel()
        {
            Client.GetBeerAsyncResponse = () =>
                new Response<Beer> { Payload = getTestBeer(), Status = HttpStatusCode.OK };
            var viewModel = GetViewModel(new BeerParameters(42));

            Client.GetBeerAsyncResponse = null;

            viewModel.FavoriteOperationInProgress.AllowToComplete();

            return viewModel;
        }

        private Beer getTestBeer()
        {
            return new Beer
                       {
                           Id = 42,
                           Name = "Duff Dark",
                           Brewery = new Brewery {Id = 314, Name = "Duff"}
                       };
        }
    }
}