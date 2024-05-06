using Application.Dto.Models.Security;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Test.Application.NUnitTest.Core;

namespace Test.Application.NUnitTest.Integration.Security.Role
{
    public class PostRoleSecurityIntegrationTest : TestNUnitBase
    {
        [SetUp]
        public void Setup()
        {
            TestContext.Out.WriteLine("SetUp - Setup");
            _url = "api/v1.0/security/role";
        }

        [Test]
        [TestCase("RolePostOk")]
        public async Task PostRoleSecurityIntegrationTest_ReturnsStatusCodeCreated(string name)
        {
            using var scope = _contextWebApp.Services.CreateScope();

            TestContext.Out.WriteLine($"_url: {_url}");
            // Arrange
            var client = await GetClient();

            RoleDto roleDto = new RoleDto() {
                Name = name,
            };

            // Act
            var result = await client.PostAsJsonAsync<RoleDto>(_url, roleDto);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        [TestCase("RolePostConflict")]
        public async Task PostRoleSecurityIntegrationTest_ReturnsStatusCodeCreatedConflict(string name)
        {
            // Arrange
            var client = await GetClient();

            RoleDto roleDto = new RoleDto()
            {
                Name = name,
            };

            // Act
            await client.PostAsJsonAsync<RoleDto>(_url, roleDto);
            var result = await client.PostAsJsonAsync<RoleDto>(_url, roleDto);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Conflict);
        }
    }
}
