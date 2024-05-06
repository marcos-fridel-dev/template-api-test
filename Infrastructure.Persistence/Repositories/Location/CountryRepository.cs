using Domain.Models.Location;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Infrastructure.Persistence.Repositories.Location
{
    public class CountryRepository : Repository<Country>, IRepository<Country>
    {
        public CountryRepository(AppDbContext context) : base(context) { }

        public override Func<IQueryable<Country>, IOrderedQueryable<Country>> OrdenBy => x => x.OrderBy(o => o.Name);
        public override IQueryable<Country> QueryInclude =>
            this.Entity
                .Include(x => x.States.Where(x => !x.IsDeleted).OrderBy(o => o.Name))
                .Include(x => x.Currencies.Where(x => !x.IsDeleted).OrderBy(o => o.Name));
    }
}