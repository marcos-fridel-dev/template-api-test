namespace Infrastructure.Services.Interfaces.Environment
{
    public interface ISettingsEnvironmentUrls
    {
        public string Http { get; init; }
        public string Https { get; init; }
        public string PokemonSample { get; init; }
    }
}
