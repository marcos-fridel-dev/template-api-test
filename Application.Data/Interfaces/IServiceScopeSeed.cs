using System.Threading.Tasks;

namespace Application.Data.Interfaces 
{
    public interface IServiceScopeSeed
    {
        Task<bool> Process();
    }

}

