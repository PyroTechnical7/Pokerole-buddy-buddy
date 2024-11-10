using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PokeroleBuddyHelper.Models
{
    public class PokemonCollectionWrapper
    {
        [JsonPropertyName("pokemonCollection")]
        public List<Pokemon>? PokemonCollection { get; set; }
    }
}
