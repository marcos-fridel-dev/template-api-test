using Domain.Models.Location;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Infrastructure.Persistence.Repositories.Location
{
    public class CurrencyRepository : Repository<Currency>, IRepository<Currency>
    {
        public CurrencyRepository(AppDbContext context) : base(context) { }

        public override Func<IQueryable<Currency>, IOrderedQueryable<Currency>> OrdenBy => x => x.OrderBy(o => o.Name);
        public override IQueryable<Currency> QueryInclude =>
            this.Entity
                .Include(x => x.Country);
    }
}