using System;

namespace Application.Services.Sample.Http.Pokemon.Models
{
    public class GetByIdSpritePokemonSample
    {
        public string back_default { get; set; }
    }

    public class GetByIdPokemonSample
    {
        public int base_experience { get; set; }
        public Guid id { get; set; }
        public bool is_default { get; set; }
        public string name { get; set; }
        public int order { get; set; }
        public GetByIdSpritePokemonSample sprites { get; set; }
    }
}