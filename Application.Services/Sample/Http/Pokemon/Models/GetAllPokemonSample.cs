using System.Collections.Generic;

namespace Application.Services.Sample.Http.Pokemon.Models
{
    public class GetAllItemPokemonSample
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class GetAllPokemonSample
    {
        public int count { get; set; }
        public string? next { get; set; }
        public string? previous { get; set; }
        public IEnumerable<GetAllItemPokemonSample> results { get; set; }
    }
}