using Domain.Models.Location;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Infrastructure.Persistence.Repositories.Location
{
    public class CityRepository : Repository<City>, IRepository<City>
    {
        public CityRepository(AppDbContext context) : base(context) { }

        public override Func<IQueryable<City>, IOrderedQueryable<City>> OrdenBy => x => x.OrderBy(o => o.Name);
        public override IQueryable<City> QueryInclude =>
            this.Entity
                .Include(x => x.State);
    }
}