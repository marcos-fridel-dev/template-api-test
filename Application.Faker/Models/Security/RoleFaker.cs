using Bogus;
using Domain.Models.Security;
using System;
using System.Collections.Generic;

namespace Application.Faker.Models.Security
{
    public static class RoleFaker
    {
        public static List<Role> GenerateData(int count = 100)
        {
            Faker<Role> faker = new Faker<Role>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Name, f => $"{f.Name.JobArea()}-{Guid.NewGuid()}");

            return faker.Generate(count);
        }
    }
}