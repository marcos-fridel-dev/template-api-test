using Application.Dto.Models.Security;
using Application.UseCases.Security.Role.Queries.GetAllRoleQuery;
using Infrastructure.Persistence.Enums.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Application.NUnitTest.Core;

namespace Test.Application.NUnitTest.Units.UsesCases.Security.Role
{
    public class GetAllRoleUseCaseUnitTest() : TestNUnitBase
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
            List<RoleDto> result = (await SendAsync(new GetAllRoleUseCase(pageNumber, pageSize, isDeleted))).ToList();

            // Assert
            Assert.That(result.Count(), Is.GreaterThan(0));
        }
    }
}
