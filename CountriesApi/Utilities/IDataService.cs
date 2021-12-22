using System.Collections.Generic;
using CountriesApi.Models;

namespace CountriesApi.Utilities
{
    public interface IDataService
    {
        IEnumerable<Country> GetAll();

        Country Get(string id);

        void Post(Country country);

        void Put(string id, Country country);

        void Delete(string id);
    }
}