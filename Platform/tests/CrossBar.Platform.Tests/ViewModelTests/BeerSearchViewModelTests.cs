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
    public class BeerSearchViewModelTests : ViewModelTestsBase<BeerSearchViewModel, EmptyParameters>
    {
        [Test]
        public void SearchCommand_NoQueryEntered_DoesNotSearch()
        {
            var beerSearch = GetViewModel(null);
            var notifications = new List<string>();
            beerSearch.Query = "";

            beerSearch.PropertyChanged += (s, e) => notifications.Add(e.PropertyName);

            Assert.False(beerSearch.IsSearching);
            Assert.AreEqual(0, notifications.Count);
        }

        [Test]
        public void SearchCommand_WithQuery_PerformsSearch()
        {
            var beerSearch = GetViewModel(null);
            var notifications = new List<string>();
            var beers = new List<Beer> {new Beer {Id = 42, Name = "Duff Dark"}};

            Client.ListBeersAsyncResponse =
                () => new Response<BeerList> {Payload = new BeerList { Beers = beers }, Status = HttpStatusCode.OK};

            beerSearch.Query = "duff";
            beerSearch.PropertyChanged += (s, e) => notifications.Add(e.PropertyName);

            beerSearch.SearchCommand.Execute(null);
            beerSearch.IsSearching.AllowToComplete();

            Assert.False(beerSearch.IsSearching);
            Assert.AreEqual(2, notifications.Count(notification => notification == "IsSearching"));
            Assert.AreEqual(beers, beerSearch.Beers);
        }

        [Test]
        public void SelectBeerCommand_NotAlreadyLoading_NavigatesToBeer()
        {
            var beerSearch = GetViewModel(null);
            var beer = new Beer {Id = 42, Name = "Duff Dark"};

            beerSearch.SelectBeerCommand.Execute(beer);
            beerSearch.IsSearching.AllowToComplete();

            Assert.AreEqual(1, Dispatcher.NavigateRequests.Count);
            Assert.That(Dispatcher.NavigateRequests.First().ViewModelType == typeof (BeerViewModel));
        }
    }
}