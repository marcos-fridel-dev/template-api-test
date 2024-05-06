using Domain.Models.Security;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.Security
{
    public class UserRepository : Repository<User>, IRepository<User>
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public override Func<IQueryable<User>, IOrderedQueryable<User>> OrdenBy => x => x.OrderBy(o => o.LastName);
        public override IQueryable<User> QueryInclude =>
            this.Entity
                .Include(x => x.Roles);

        public async Task<User?> FindByNameAsync(string userName) =>
            await QueryInclude.FirstOrDefaultAsync(x => x.UserName == userName);
    }
}