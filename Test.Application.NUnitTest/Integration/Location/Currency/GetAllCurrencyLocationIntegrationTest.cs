using Application.Dto.Models.Security;
using Shared.Common.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Test.Application.NUnitTest.Core;

namespace Test.Application.NUnitTest.Integration.Location.Currency
{
    public class GetAllCurrencyLocationIntegrationTest() : TestNUnitBase
    {
        [SetUp]
        public void Setup()
        {
            TestContext.Out.WriteLine("SetUp - Setup");
            _url = "api/v1.0/location/currency";
        }

        [Test]
        public async Task GetAllCurrency_ReturnsArrayWithOrWithoutElements()
        {
            // Arrange
            var client = await GetClient(true);

            // Act
            var result = await client.GetFromJsonAsync<ResultResponse<List<RoleDto>>>(_url);

            // Assert
            Assert.That(result.Data.Count(), Is.GreaterThan(0));
        }

        [Test]
        public async Task GetAllCurrency_ReturnsStatusCodeUnauthorized()
        {
            // Arrange
            var client = await GetClient();

            // Act
            var result = await client.GetAsync(_url);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Unauthorized);
        }

    }
}
