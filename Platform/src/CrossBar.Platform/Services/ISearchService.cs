using System.Collections.Generic;
using System.Threading.Tasks;
using Amarillo.Entities;

namespace CrossBar.Platform.Services
{
    public interface ISearchService
    {
        Task<IList<Beer>> FindBeers(string query);
        Task<Beer> GetBeer(int id);
        Task<IList<Brewery>> FindBreweries(string query);
        Task<Brewery> GetBrewery(int id);
    }
}