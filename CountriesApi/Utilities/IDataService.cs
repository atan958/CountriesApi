using System.Collections.Generic;
using CountriesApi.Models;

namespace CountriesApi.Utilities
{
    public interface IDataService
    {
        IEnumerable<Country> GetAll();
        Country Get(string id);
    }
}