using Application.Services.Sample.Http.Pokemon.Models;
using MediatR;
using System;

namespace Application.UseCases.Sample.Http.Pokemon.Queries.GetByIdPokemonHttpQuery
{
    public sealed class GetByIdPokemonHttpUseCase(Guid _id) : IRequest<GetByIdPokemonSample>
    {
        public Guid Id => _id;
    }
}