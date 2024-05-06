using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Interfaces.Context
{
    public interface IWithOnModelCreating
    {
        void OnCreating(ModelBuilder modelBuilder);
    }
}