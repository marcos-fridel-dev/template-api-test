using Bogus;
using Domain.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Faker.Models.Security
{
    public static class UserFaker
    {
        public static List<User> GenerateData(List<Role> roles, int count = 100)
        {
            Random random = new Random();
            Faker<User> faker = new Faker<User>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.FirstName, f => $"{f.Name.FirstName()}")
                .RuleFor(u => u.LastName, f => $"{f.Name.LastName()}")
                .RuleFor(u => u.UserName, f => $"{f.Name.FirstName()}-{Guid.NewGuid()}")
                .RuleFor(u => u.Email, f => $"{f.Internet.Email()}")
                .RuleFor(u => u.Roles, f => [ roles.ElementAt(random.Next(0, roles.Count())) ]);

            return faker.Generate(count);
        }
    }
}