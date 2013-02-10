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
            var beer = new Beer {Id = beerId, Name = "Duffs"};
            Client.GetBeerAsyncResponse =
                () => new Response<Beer> {Payload = beer, Status = HttpStatusCode.OK};

            var result = Service.GetBeer(beerId).Test();

            Assert.AreEqual(beer, result);
        }

        [Test]
        public void GetBeer_CalledTwiceForSameBeer_UsesCacheForSecondCall()
        {
            int beerId = 42;
            var beer = new Beer { Id = beerId, Name = "Duffs" };
            Client.GetBeerAsyncResponse =
                () => new Response<Beer> { Payload = beer, Status = HttpStatusCode.OK };

            var result = Service.GetBeer(beerId).Test();

            Assert.AreEqual(beer, result);

            Client.GetBeerAsyncResponse = delegate { throw new InvalidOperationException(); };

            result = Service.GetBeer(beerId).Test();

            Assert.AreEqual(beer, result);
        }

        [Test]
        public void FindBeers_ValidQuery_ReturnsListOfBeers()
        {
            string query = "duff";
            var beers = new List<Beer> {new Beer {Id = 42, Name = "Duffs"}};
            Client.ListBeersAsyncResponse =
                () => new Response<BeerList> {Payload = new BeerList {Beers = beers}, Status = HttpStatusCode.OK};

            var result = Service.FindBeers(query).Test();

            Assert.AreEqual(beers, result);
        }
    }
}
