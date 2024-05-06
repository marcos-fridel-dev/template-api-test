using Application.Dto.Models.Security;
using Shared.Common.Models.Responses;
using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Test.Application.NUnitTest.Core;
using TEntity = Domain.Models.Security.Role;

namespace Test.Application.NUnitTest.Integration.Security.Role
{
    public class GetByIdRoleSecurityIntegrationTest : TestNUnitBase
    {
        [SetUp]
        public void Setup()
        {
            TestContext.Out.WriteLine("SetUp - Setup");
            _url = "api/v1.0/security/role";
        }

        [Test]
        public async Task GetByIdRoleSecurityIntegrationTest_ReturnsRoleExists()
        {
            // Arrange
            var client = await GetClient();
            TEntity role = await FirstOrDefaultAsync<TEntity>();
            _url = $"{_url}/{role.Id}";
            TestContext.Out.WriteLine($"_url: {_url}");

            // Act
            var result = await client.GetFromJsonAsync<ResultResponse<RoleDto>>(_url);

            // Assert
            Assert.IsNotNull(result.Data);
        }

        [Test]
        [TestCase("00000000-0000-0000-0000-000000000000")]
        public async Task GetByIdRoleSecurityIntegrationTest_ReturnsRoleNotExists(Guid id)
        {
            // Arrange
            var client = await GetClient();
            _url = $"{_url}/{id}";
            TestContext.Out.WriteLine($"_url: {_url}");

            // Act
            var result = await client.GetAsync($"{_url}/{id}");
            
            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);
        }
    }
}
