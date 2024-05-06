using Application.Services.Sample.Http.Pokemon.Services;
using Infrastructure.Services.Models.Environment;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Application.Services.Sample.Http.Pokemon.Configuration
{
    public static class PokemonHttpSampleExtension
    {
        public static IServiceCollection AddPokemonHttpSampleService(this IServiceCollection services, SettingsEnvironment env)
        {
            services.AddHttpClient<PokemonService>(c =>
            {
                c.BaseAddress = new Uri(env.Urls.PokemonSample);
            });

            return services;
        }
    }
}