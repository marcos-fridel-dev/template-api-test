using Application.Services.Sample.Http.Pokemon.Constants;
using Application.Services.Sample.Http.Pokemon.Intefaces;
using Application.Services.Sample.Http.Pokemon.Models;
using Infrastructure.Services.Extensions.Services;
using Microsoft.Extensions.Localization;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services.Sample.Http.Pokemon.Services
{
    public class PokemonService(HttpClient _httpClient, IStringLocalizer _localizer) : IPokemonService
    {
        public async Task<GetAllPokemonSample?> GetAllAsync(int limit = 40)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{Endpoints.GetAll}{limit}");

            response.ValidateHttpStatusCodeAndThrowException(_localizer);

            return JsonSerializer.Deserialize<GetAllPokemonSample>(await response.Content.ReadAsStringAsync());
        }

        public async Task<GetByIdPokemonSample?> GetByIdAsync(Guid id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{Endpoints.GetById}{id}");

            response.ValidateHttpStatusCodeAndThrowException(_localizer);

            return JsonSerializer.Deserialize<GetByIdPokemonSample>(await response.Content.ReadAsStringAsync());
        }
    }
}