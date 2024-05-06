using Domain.Models.Authentication;
using Domain.Models.Location;
using Domain.Models.Security;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context
{
    public partial class AppDbContext : DbContext, IAppDbContext
    {
        #region AUTHENTICATION
        public DbSet<Login> Login { get; set; }
        #endregion

        #region LOCATION
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<State> States { get; set; }

        #endregion

        #region SECURITY
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion
    }
}