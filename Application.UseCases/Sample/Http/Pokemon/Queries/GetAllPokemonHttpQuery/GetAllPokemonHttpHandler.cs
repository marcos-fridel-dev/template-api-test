using Application.Services.Sample.Http.Pokemon.Models;
using Application.Services.Sample.Http.Pokemon.Services;
using Infrastructure.Services.Models.Environment;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Sample.Http.Pokemon.Queries.GetAllPokemonHttpQuery
{
    public class GetAllPokemonHttpHandler(PokemonService _pokemonService, SettingsEnvironment _settings) : IRequestHandler<GetAllPokemonHttpUseCase, GetAllPokemonSample>
    {
        public async Task<GetAllPokemonSample> Handle(GetAllPokemonHttpUseCase request, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"settings: {_settings.Database.ConnectionString}");
            return await _pokemonService.GetAllAsync(40);
        }
    }
}