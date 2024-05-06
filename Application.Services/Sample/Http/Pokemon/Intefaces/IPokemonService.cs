using Application.Services.Sample.Http.Pokemon.Models;
using System;
using System.Threading.Tasks;

namespace Application.Services.Sample.Http.Pokemon.Intefaces
{
    public interface IPokemonService
    {
        Task<GetAllPokemonSample?> GetAllAsync(int limit = 40);
        Task<GetByIdPokemonSample?> GetByIdAsync(Guid id);
    }
}