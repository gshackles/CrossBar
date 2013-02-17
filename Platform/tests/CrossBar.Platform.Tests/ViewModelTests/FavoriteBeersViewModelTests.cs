using System.Linq;
using CrossBar.Platform.DataAccess.Entities;
using CrossBar.Platform.ViewModels;
using CrossBar.Platform.ViewModels.Parameters;
using NUnit.Framework;

namespace CrossBar.Platform.Tests.ViewModelTests
{
    public class FavoriteBeersViewModelTests : ViewModelTestsBase<FavoriteBeersViewModel, EmptyParameters>
    {
        [Test]
        public void SelectFavoriteCommand_NavigatesToBeer()
        {
            var favorite = new FavoriteBeer {Id = 1, BeerId = 42, Name = "Duff Dark"};
            var viewModel = GetViewModel(null);

            viewModel.SelectFavoriteCommand.Execute(favorite);

            Assert.AreEqual(1, Dispatcher.NavigateRequests.Count);
            Assert.That(Dispatcher.NavigateRequests.First().ViewModelType == typeof(BeerViewModel));
        }
    }
}