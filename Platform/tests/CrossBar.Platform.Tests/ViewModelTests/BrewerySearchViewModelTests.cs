using System.Collections.Generic;
using System.Linq;
using System.Net;
using Amarillo.Entities;
using Amarillo.Network;
using Amarillo.Payloads;
using CrossBar.Platform.Tests.Extensions;
using CrossBar.Platform.ViewModels;
using CrossBar.Platform.ViewModels.Parameters;
using NUnit.Framework;

namespace CrossBar.Platform.Tests.ViewModelTests
{
    public class BrewerySearchViewModelTests : ViewModelTestsBase<BrewerySearchViewModel, EmptyParameters>
    {
        [Test]
        public void SearchCommand_NoQueryEntered_DoesNotSearch()
        {
            var brewerySearch = GetViewModel(null);
            var notifications = new List<string>();
            brewerySearch.Query = "";

            brewerySearch.PropertyChanged += (s, e) => notifications.Add(e.PropertyName);

            Assert.False(brewerySearch.IsSearching);
            Assert.AreEqual(0, notifications.Count);
        }

        [Test]
        public void SearchCommand_WithQuery_PerformsSearch()
        {
            var brewerySearch = GetViewModel(null);
            var notifications = new List<string>();
            var breweries = new List<Brewery> {new Brewery {Id = 42, Name = "Duff"}};

            Client.ListBreweriesAsyncResponse =
                () => new Response<BreweryList> { Payload = new BreweryList { Breweries = breweries }, Status = HttpStatusCode.OK };

            brewerySearch.Query = "duff";
            brewerySearch.PropertyChanged += (s, e) => notifications.Add(e.PropertyName);

            brewerySearch.SearchCommand.Execute(null);
            brewerySearch.IsSearching.AllowToComplete();

            Assert.False(brewerySearch.IsSearching);
            Assert.AreEqual(2, notifications.Count(notification => notification == "IsSearching"));
            Assert.AreEqual(breweries, brewerySearch.Breweries);
        }

        [Test]
        public void SelectBreweryCommand_NotAlreadyLoading_NavigatesToBrewery()
        {
            var brewerySearch = GetViewModel(null);
            var brewery = new Brewery {Id = 42, Name = "Duff"};

            brewerySearch.SelectBreweryCommand.Execute(brewery);
            brewerySearch.IsSearching.AllowToComplete();

            Assert.AreEqual(1, Dispatcher.NavigateRequests.Count);
            Assert.That(Dispatcher.NavigateRequests.First().ViewModelType == typeof(BreweryViewModel));
        }
    }
}