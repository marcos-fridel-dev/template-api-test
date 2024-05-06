using Application.Services.Sample.Http.Pokemon.Models;
using MediatR;

namespace Application.UseCases.Sample.Http.Pokemon.Queries.GetAllPokemonHttpQuery
{
    public sealed class GetAllPokemonHttpUseCase() : IRequest<GetAllPokemonSample>
    {
    }
}