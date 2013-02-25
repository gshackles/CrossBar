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
    public class BreweryViewModelTests : ViewModelTestsBase<BreweryViewModel, BreweryParameters>
    {
        [Test]
        public void ViewModelLoaded_BreweryIsFavorite_MarkedAsFavorite()
        {
            var brewery = getTestBrewery();
            FavoriteBreweryRepository.SaveFavorite(brewery.Id, brewery.Name).Test();

            Assert.AreEqual(1, FavoriteBreweryRepository.ListFavoriteBreweries().Test().Count);

            var viewModel = getTestViewModel();

            Assert.IsTrue(viewModel.IsFavorite);
        }

        [Test]
        public void ViewModelLoaded_BreweryIsNotFavorite_NotMarkedAsFavorite()
        {
            var viewModel = getTestViewModel();

            Assert.AreEqual(0, FavoriteBreweryRepository.ListFavoriteBreweries().Test().Count);
            Assert.IsFalse(viewModel.IsFavorite);
        }

        [Test]
        public void ToggleFavoriteCommand_BreweryIsFavorite_FavoriteIsRemoved()
        {
            var brewery = getTestBrewery();
            FavoriteBreweryRepository.SaveFavorite(brewery.Id, brewery.Name).Test();

            Assert.AreEqual(1, FavoriteBreweryRepository.ListFavoriteBreweries().Test().Count);

            var viewModel = getTestViewModel();

            Assert.IsTrue(viewModel.IsFavorite);

            viewModel.ToggleFavoriteCommand.Execute(null);
            viewModel.FavoriteOperationInProgress.AllowToComplete();

            Assert.IsFalse(viewModel.IsFavorite);
            Assert.AreEqual(0, FavoriteBreweryRepository.ListFavoriteBreweries().Test().Count);
        }

        [Test]
        public void ToggleFavoriteCommand_BreweryIsNotFavorite_SavesFavorite()
        {
            var viewModel = getTestViewModel();

            Assert.IsFalse(viewModel.IsFavorite);
            Assert.AreEqual(0, FavoriteBreweryRepository.ListFavoriteBreweries().Test().Count);

            viewModel.ToggleFavoriteCommand.Execute(null);
            viewModel.FavoriteOperationInProgress.AllowToComplete();

            Assert.IsTrue(viewModel.IsFavorite);
            Assert.AreEqual(1, FavoriteBreweryRepository.ListFavoriteBreweries().Test().Count);
        }

        [Test]
        public void GoToBrewerySiteCommand_NavigatesToBrewerySite()
        {
            var viewModel = getTestViewModel();

            viewModel.GoToBrewerySiteCommand.Execute(null);

            Assert.AreEqual(1, WebBrowserTask.UrlRequests.Count);
            Assert.AreEqual(viewModel.Brewery.Url, WebBrowserTask.UrlRequests.First());
        }

        private BreweryViewModel getTestViewModel()
        {
            Client.GetBreweryAsyncResponse = () =>
                new Response<Brewery> { Payload = getTestBrewery(), Status = HttpStatusCode.OK };
            var viewModel = GetViewModel(new BreweryParameters(42));

            Client.GetBreweryAsyncResponse = null;

            viewModel.FavoriteOperationInProgress.AllowToComplete();

            return viewModel;
        }

        private Brewery getTestBrewery()
        {
            return new Brewery
            {
                Id = 42,
                Name = "Duff",
                Url = "http://www.brewery.com"
            };
        }
    }
}