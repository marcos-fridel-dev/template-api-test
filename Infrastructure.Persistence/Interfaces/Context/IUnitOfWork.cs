using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Interfaces.Context
{
    public partial interface IUnitOfWork
    {
        Task<int> SaveAsync();
        Task<int> SaveAsync(CancellationToken cancellationToken);

        //Repository<EntitySample> EntitiesSample { get; }
    }
}