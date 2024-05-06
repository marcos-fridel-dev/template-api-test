using Application.Services.Sample.Http.Pokemon.Models;
using Application.Services.Sample.Http.Pokemon.Services;
using AutoMapper;
using MediatR;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Sample.Http.Pokemon.Queries.GetByIdPokemonHttpQuery
{
    public class GetByIdPokemonHttpHandler(IMapper _mapper, PokemonService _pokemonService, HttpClient httpClient) : IRequestHandler<GetByIdPokemonHttpUseCase, GetByIdPokemonSample>
    {
        public async Task<GetByIdPokemonSample?> Handle(GetByIdPokemonHttpUseCase request, CancellationToken cancellationToken = default)
        {
            GetByIdPokemonSample result = await _pokemonService.GetByIdAsync(request.Id);
            byte[] bytes = await httpClient.GetByteArrayAsync(result.sprites.back_default);
            result.sprites.back_default = Convert.ToBase64String(bytes);

            return result;

        }
    }
}