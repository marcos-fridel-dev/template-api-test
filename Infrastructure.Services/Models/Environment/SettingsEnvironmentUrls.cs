using Infrastructure.Services.Interfaces.Environment;

namespace Infrastructure.Services.Models.Environment
{
    public class SettingsEnvironmentUrls: ISettingsEnvironmentUrls
    {
        public string Http { get; init; }
        public string Https { get; init; }
        public string PokemonSample { get; init; }
    }
}
