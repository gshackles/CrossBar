using System;
using System.Collections.Generic;
using System.Linq;
using CrossBar.Platform.ViewModels;
using CrossBar.Platform.ViewModels.Parameters;
using NUnit.Framework;

namespace CrossBar.Platform.Tests.ViewModelTests
{
    public class MainMenuViewModelTests : ViewModelTestsBase<MainMenuViewModel, EmptyParameters>
    {
        [Test]
        public void FindBeersCommand_NavigatesToBeerSearch()
        {
            var mainMenu = GetViewModel(null);

            mainMenu.FindBeersCommand.Execute(null);

            Assert.AreEqual(1, Dispatcher.NavigateRequests.Count);
            Assert.That(Dispatcher.NavigateRequests.First().ViewModelType == typeof(BeerSearchViewModel));
        }

        [Test]
        public void FindBreweriesCommand_NavigatesToBrewerySearch()
        {
            var mainMenu = GetViewModel(null);

            mainMenu.FindBreweriesCommand.Execute(null);

            Assert.AreEqual(1, Dispatcher.NavigateRequests.Count);
            Assert.That(Dispatcher.NavigateRequests.First().ViewModelType == typeof(BrewerySearchViewModel));
        }
    }
}
