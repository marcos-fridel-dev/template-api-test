using Domain.Models.Location;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context
{
    public partial class AppDbContext : DbContext, IAppDbContext
    {

        private void OnModelCreatingLocationState(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<State>()
            //    .HasOne(x => x.Country)
            //    .WithMany(x => x.States)
            //    .HasForeignKey(s => s.CountryId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}