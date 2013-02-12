using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Amarillo.Entities;
using Amarillo.Network;
using Amarillo.Payloads;
using CrossBar.Platform.Services;
using CrossBar.Platform.Tests.Extensions;
using NUnit.Framework;

namespace CrossBar.Platform.Tests.ServiceTests
{
    public class SearchServiceTests : ServiceTestsBase<ISearchService>
    {
        [Test]
        public void GetBeer_ValidBeer_ReturnsBeer()
        {
            int beerId = 42;
            var beer = new Beer { Id = beerId, Name = "Duff Dark" };
            Client.GetBeerAsyncResponse =
                () => new Response<Beer> {Payload = beer, Status = HttpStatusCode.OK};

            var result = Service.GetBeer(beerId).Test();

            Assert.AreEqual(beer, result);
        }

        [Test]
        public void GetBeer_ErrorThrown_ReturnsNullAndReportsError()
        {
            Client.GetBeerAsyncResponse =
                () => { throw new ApplicationException(); };

            var result = Service.GetBeer(42).Test();

            Assert.IsNull(result);
            Assert.AreEqual(1, ErrorReporter.ErrorMessages.Count);
            Assert.NotNull(ErrorReporter.ErrorMessages.First());
        }

        [Test]
        public void GetBeer_CalledTwiceForSameBeer_UsesCacheForSecondCall()
        {
            int beerId = 42;
            var beer = new Beer { Id = beerId, Name = "Duff Dark" };
            Client.GetBeerAsyncResponse =
                () => new Response<Beer> { Payload = beer, Status = HttpStatusCode.OK };

            var result = Service.GetBeer(beerId).Test();

            Assert.AreEqual(beer, result);

            Client.GetBeerAsyncResponse = delegate { throw new InvalidOperationException(); };

            result = Service.GetBeer(beerId).Test();

            Assert.AreEqual(beer, result);
        }

        [Test]
        public void GetBeer_ResultsHaveBreweryInfo_BreweryGetsCached()
        {
            int beerId = 42;
            var brewery = new Brewery {Id = 314, Name = "Duff"};
            var beer = new Beer {Id = beerId, Name = "Duff Dark", Brewery = brewery};
            Client.GetBeerAsyncResponse =
                () => new Response<Beer> { Payload = beer, Status = HttpStatusCode.OK };

            var result = Service.GetBeer(beerId).Test();

            Assert.AreEqual(beer, result);

            Client.GetBreweryAsyncResponse = delegate { throw new InvalidOperationException(); };

            var breweryResult = Service.GetBrewery(brewery.Id).Test();

            Assert.AreEqual(brewery, breweryResult);
        }

        [Test]
        public void FindBeers_ValidQuery_ReturnsListOfBeers()
        {
            string query = "duff";
            var beers = new List<Beer> {new Beer {Id = 42, Name = "Duff Dark"}};
            Client.ListBeersAsyncResponse =
                () => new Response<BeerList> {Payload = new BeerList {Beers = beers}, Status = HttpStatusCode.OK};

            var result = Service.FindBeers(query).Test();

            Assert.AreEqual(beers, result);
        }

        [Test]
        public void FindBeers_ErrorThrown_ReturnsNullAndReportsError()
        {
            Client.ListBeersAsyncResponse =
                () => { throw new ApplicationException(); };

            var result = Service.FindBeers("duff").Test();

            Assert.IsNull(result);
            Assert.AreEqual(1, ErrorReporter.ErrorMessages.Count);
            Assert.NotNull(ErrorReporter.ErrorMessages.First());
        }

        [Test]
        public void FindBeers_ResultsHaveBreweryInfo_BreweryGetsCached()
        {
            string query = "duff";
            var brewery = new Brewery { Id = 314, Name = "Duff" };
            var beers = new List<Beer> {new Beer {Id = 42, Name = "Duff Dark", Brewery = brewery}};
            Client.ListBeersAsyncResponse =
                () => new Response<BeerList> { Payload = new BeerList { Beers = beers }, Status = HttpStatusCode.OK };

            var result = Service.FindBeers(query).Test();

            Assert.AreEqual(beers, result);

            Client.GetBreweryAsyncResponse = delegate { throw new InvalidOperationException(); };

            var breweryResult = Service.GetBrewery(brewery.Id).Test();

            Assert.AreEqual(brewery, breweryResult);
        }

        [Test]
        public void GetBrewery_ValidBrewery_ReturnsBrewery()
        {
            int breweryId = 42;
            var brewery = new Brewery {Id = 42, Name = "Duff"};
            Client.GetBreweryAsyncResponse =
                () => new Response<Brewery> { Payload = brewery, Status = HttpStatusCode.OK };

            var result = Service.GetBrewery(breweryId).Test();

            Assert.AreEqual(brewery, result);
        }

        [Test]
        public void GetBrewery_ErrorThrown_ReturnsNullAndReportsError()
        {
            Client.GetBreweryAsyncResponse =
                () => { throw new ApplicationException(); };

            var result = Service.GetBrewery(42).Test();

            Assert.IsNull(result);
            Assert.AreEqual(1, ErrorReporter.ErrorMessages.Count);
            Assert.NotNull(ErrorReporter.ErrorMessages.First());
        }

        [Test]
        public void GetBrewery_CalledTwiceForSameBrewery_UsesCacheForSecondCall()
        {
            int breweryId = 42;
            var brewery = new Brewery { Id = 42, Name = "Duff" };
            Client.GetBreweryAsyncResponse =
                () => new Response<Brewery> { Payload = brewery, Status = HttpStatusCode.OK };

            var result = Service.GetBrewery(breweryId).Test();

            Assert.AreEqual(brewery, result);

            Client.GetBreweryAsyncResponse = delegate { throw new InvalidOperationException(); };

            result = Service.GetBrewery(breweryId).Test();

            Assert.AreEqual(brewery, result);
        }

        [Test]
        public void FindBreweries_ValidQuery_ReturnsListOfBreweries()
        {
            string query = "duff";
            var breweries = new List<Brewery> {new Brewery {Id = 42, Name = "Duff"}};
            Client.ListBreweriesAsyncResponse =
                () => new Response<BreweryList> { Payload = new BreweryList { Breweries = breweries }, Status = HttpStatusCode.OK };

            var result = Service.FindBreweries(query).Test();

            Assert.AreEqual(breweries, result);
        }

        [Test]
        public void FindBreweries_ErrorThrown_ReturnsNullAndReportsError()
        {
            Client.ListBreweriesAsyncResponse =
                () => { throw new ApplicationException(); };

            var result = Service.FindBreweries("duff").Test();

            Assert.IsNull(result);
            Assert.AreEqual(1, ErrorReporter.ErrorMessages.Count);
            Assert.NotNull(ErrorReporter.ErrorMessages.First());
        }
    }
}
