using System.Collections.Generic;

namespace PokeroleBuddyHelper.Models
{
    public class MoveCollection
    {
        [System.Text.Json.Serialization.JsonPropertyName("moveCollection")]
        public List<Move>? Moves { get; set; }
    }
}
