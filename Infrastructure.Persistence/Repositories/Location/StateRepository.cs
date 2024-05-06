using Domain.Models.Location;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Infrastructure.Persistence.Repositories.Location
{
    public class StateRepository : Repository<State>, IRepository<State>
    {
        public StateRepository(AppDbContext context) : base(context) { }

        public override Func<IQueryable<State>, IOrderedQueryable<State>> OrdenBy => x => x.OrderBy(o => o.Name);
        public override IQueryable<State> QueryInclude => this.Entity
                .Include(x => x.Country)
                .Include(x => x.Cities.Where(x => !x.IsDeleted).OrderBy(o => o.Name));
    }
}