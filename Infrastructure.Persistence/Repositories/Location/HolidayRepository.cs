using Domain.Models.Location;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Infrastructure.Persistence.Repositories.Location
{
    public class HolidayRepository : Repository<Holiday>, IRepository<Holiday>
    {
        public HolidayRepository(AppDbContext context) : base(context) { }

        public override Func<IQueryable<Holiday>, IOrderedQueryable<Holiday>> OrdenBy => x => x.OrderBy(o => o.Name);
        public override IQueryable<Holiday> QueryInclude =>
            this.Entity
                .Include(x => x.Country);
    }
}