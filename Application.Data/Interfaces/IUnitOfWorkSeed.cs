using System.Threading.Tasks;

namespace Application.Data.Interfaces
{
    public interface IUnitOfWorkSeed
    {
        Task<bool> Process();
    }

}
