using System.Collections.Generic;

namespace PokeroleBuddyHelper.Models
{
    public class AbilityCollection
    {
        [System.Text.Json.Serialization.JsonPropertyName("abilityCollection")]
        public List<Ability>? Abilities { get; set; }
    }
}
