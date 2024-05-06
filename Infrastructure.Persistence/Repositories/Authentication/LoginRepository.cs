using Domain.Models.Authentication;
using Domain.Models.Security;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.Authentication
{
    public class LoginRepository : Repository<Login>, IRepository<Login>
    {
        public LoginRepository(AppDbContext context) : base(context) { }

        public override Func<IQueryable<Login>, IOrderedQueryable<Login>> OrdenBy => x => x.OrderBy(o => o.Id);
        public override IQueryable<Login> QueryInclude =>
            Entity;

        public async Task<bool> ChangePasswordAsync(User user, string password)
        {
            try
            {
                List<Login> list = await Entity
                    .Where(x => x.User.Id == user.Id)
                    .ToListAsync();

                list.ForEach(x => x.IsCurrent = false);

                Entity.UpdateRange(list);

                Entity.Add(new()
                {
                    User = user,
                    Password = password,
                    IsCurrent = true
                });

                return true;
            }
            catch
            {
                throw;
            }

        }

        public async Task<bool> CheckPasswordAsync(User user, string password) =>
            await Entity.AnyAsync(x => x.User.Id == user.Id && x.Password == password);
    }
}