using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Interfaces.Context
{
    public interface IAppDbContext
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}