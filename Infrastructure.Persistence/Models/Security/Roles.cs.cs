using Domain.Models.Security;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Models.Security
{
    public class Roles : IWithOnModelCreating
    {
        public void OnCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasIndex(e => e.Name)
                .IsUnique();
        }
    }
}