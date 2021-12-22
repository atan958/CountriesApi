using System.Collections.Generic;
using System.Linq;
using CountriesApi.Controllers;
using CountriesApi.Models;
using CountriesApi.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CountriesApiTests
{
    [TestClass]
    public class CountryControllerTests
    {
        [TestMethod]
        public void GetAll_ReturnsCorrectNumberOfCountries()
        {
            var expectedCount = 2;

            var dataServiceMock = new Mock<IDataService>();
            dataServiceMock.Setup(d => d.GetAll())
                .Returns(new List<Country>
                {
                    new Country(),
                    new Country(),
                    new Country(),
                    new Country(),
                    new Country(),
                });

            var countriesController = new CountryController(dataServiceMock.Object);
            var result = countriesController.GetAll(expectedCount).Result as OkObjectResult;
            var count = (result.Value as IEnumerable<Country>).Count();

            Assert.AreEqual(expectedCount, count);
        }
    }
}
