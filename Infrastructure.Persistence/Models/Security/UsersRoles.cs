using Domain.Models.Security;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Models.Security
{
    public class UsersRoles : IWithOnModelCreating
    {
        public void OnCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.Users)
                .UsingEntity<UserRole>("UsersRoles");
        }
    }
}