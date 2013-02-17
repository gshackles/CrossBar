using System.Linq;
using CrossBar.Platform.DataAccess.Entities;
using CrossBar.Platform.ViewModels;
using CrossBar.Platform.ViewModels.Parameters;
using NUnit.Framework;

namespace CrossBar.Platform.Tests.ViewModelTests
{
    public class FavoriteBreweriesViewModelTests : ViewModelTestsBase<FavoriteBreweriesViewModel, EmptyParameters>
    {
        [Test]
        public void SelectFavoriteCommand_NavigatesToBrewery()
        {
            var favorite = new FavoriteBrewery {Id = 1, BreweryId = 42, Name = "Duff"};
            var viewModel = GetViewModel(null);

            viewModel.SelectFavoriteCommand.Execute(favorite);

            Assert.AreEqual(1, Dispatcher.NavigateRequests.Count);
            Assert.That(Dispatcher.NavigateRequests.First().ViewModelType == typeof(BreweryViewModel));
        }
    }
}