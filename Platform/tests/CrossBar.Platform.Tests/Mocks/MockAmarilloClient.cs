using System;
using System.Threading.Tasks;
using Amarillo;
using Amarillo.Entities;
using Amarillo.Network;
using Amarillo.Payloads;

namespace CrossBar.Platform.Tests.Mocks
{
    public class MockAmarilloClient : IAmarillo
    {
        public Func<Response<Beer>> GetBeerAsyncResponse { get; set; }
        public Func<Response<BeerList>> ListBeersAsyncResponse { get; set; }
        public Func<Response<BreweryList>> ListBreweriesAsyncResponse { get; set; }
        public Func<Response<Brewery>> GetBreweryAsyncResponse { get; set; }

        public Task<Response<BreweryList>> ListBreweriesAsync(string query = null, string orderBy = null, int? page = null, int? beersPerPage = null)
        {
            return Task.Factory.StartNew(ListBreweriesAsyncResponse);
        }

        public Task<Response<Brewery>> GetBreweryAsync(int id)
        {
            return Task.Factory.StartNew(GetBreweryAsyncResponse);
        }

        public Task<Response<BeerList>> ListBeersAsync(string query = null, string orderBy = null, int? page = null, int? beersPerPage = null)
        {
            return Task.Factory.StartNew(ListBeersAsyncResponse);
        }

        public Task<Response<Beer>> GetBeerAsync(int id)
        {
            return Task.Factory.StartNew(GetBeerAsyncResponse);
        }
    }
}