using Domain.Models.Security;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Interfaces.Context;
using System;
using System.Linq;

namespace Infrastructure.Persistence.Repositories.Security
{
    public class RoleRepository : Repository<Role>, IRepository<Role>
    {
        public RoleRepository(AppDbContext context) : base(context) { }

        public override Func<IQueryable<Role>, IOrderedQueryable<Role>> OrdenBy => x => x.OrderBy(o => o.Name);
        public override IQueryable<Role> QueryInclude => this.Entity;
    }
}