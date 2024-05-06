using Application.Dto.Models.Location;
using Application.UseCases.Location.Currency.Commands.CreateCurrencyCommand;
using Domain.Models.Location;
using Shared.Common.Exceptions;
using System;
using System.Threading.Tasks;
using Test.Application.NUnitTest.Core;

namespace Test.Application.NUnitTest.Units.UsesCases.Location.Currency
{
    public class CreateCurrencyUseCaseUnitTest : TestNUnitBase
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task CreateCurrencyUseCaseUnitTest_ReturnsCreatedSuccesfully()
        {
            // Arrange
            Country country = await FirstOrDefaultAsync<Country>();

            CurrencyPostDto currency = new() { 
                Name = "Test1",
                Code = "ARS1",
                Symbol = "$1",
                CountryId = country.Id,
                IsFiat = false
            };

            // Act
            CurrencyDto? result = await SendAsync(new CreateCurrencyUseCase(currency));
            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task CreateCurrencyUseCaseUnitTest_WithoutParameters_ReturnExceptionValidator()
        {
            try
            {
                // Arrange
                Country country = await FirstOrDefaultAsync<Country>();

                CurrencyPostDto currency = new()
                {
                    Code = "ARS1",
                    Symbol = "$1",
                    CountryId = country.Id,
                    IsFiat = false
                };

                // Act
                CurrencyDto? result = await SendAsync(new CreateCurrencyUseCase(currency));

                // Assert
                Assert.That(result, Is.Not.Null);
            }
            catch (Exception ex)
            {
                if (ex is ValidatorException)
                    Assert.Pass();
                else
                    Assert.Fail();
            }
        }
        
        [Test]
        public async Task CreateCurrencyUseCaseUnitTest_NameExists_ReturnsExceptionValidator()
        {
            try
            {
                // Arrange
                Country country = await FirstOrDefaultAsync<Country>();

                CurrencyPostDto currency = new()
                {
                    Name = "Test2",
                    Code = "ARS1",
                    Symbol = "$1",
                    CountryId = country.Id,
                    IsFiat = false
                };

                // Act
                CurrencyDto? result = await SendAsync(new CreateCurrencyUseCase(currency));
                result = await SendAsync(new CreateCurrencyUseCase(currency));

                // Assert
                Assert.That(result, Is.Not.Null);
            }
            catch (Exception ex)
            {
                if (ex is ValidatorException)
                    Assert.Pass();
                else
                    Assert.Fail();
            }
}
    }
}
