using Application.Dto.Models.Security;
using Shared.Common.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Test.Application.NUnitTest.Core;

namespace Test.Application.NUnitTest.Integration.Security.Role
{
    public class GetAllRoleSecurityIntegrationTest() : TestNUnitBase
    {
        [SetUp]
        public void Setup()
        {
            TestContext.Out.WriteLine("SetUp - Setup");
            _url = "api/v1.0/security/role";
        }

        [Test]
        public async Task GetAllRoleSecurityIntegrationTest_ReturnsArrayWithOrWithoutElements()
        {
            // Arrange
            var client = await GetClient();

            // Act
            var result = await client.GetFromJsonAsync<ResultResponse<List<RoleDto>>>(_url);

            // Assert
            Assert.That(result.Data.Count(), Is.GreaterThan(0));
        }
    }
}
