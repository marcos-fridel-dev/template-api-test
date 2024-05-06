using Application.Dto.Models.Location;
using Application.UseCases.Location.Currency.Queries.GetAllCurrencyQuery;
using Infrastructure.Persistence.Enums.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Application.NUnitTest.Core;

namespace Test.Application.NUnitTest.Units.UsesCases.Location.Currency
{
    public class GetAllCurrencyUseCaseUnitTest() : TestNUnitBase
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(1, 10, IsDeleted.OnlyNotDeleted)]
        public async Task GetAllRoleSecurityUnitTest_ReturnsArrayWithOrWithoutElements(int pageNumber, int pageSize, IsDeleted isDeleted)
        {
            // Arrange

            // Act
            List<CurrencyDto> result = (await SendAsync(new GetAllCurrencyUseCase(pageNumber, pageSize, isDeleted))).ToList();

            // Assert
            Assert.That(result.Count(), Is.GreaterThan(0));
        }
    }
}
