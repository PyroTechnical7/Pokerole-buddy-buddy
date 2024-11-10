using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleBuddyHelper.Models
{
    public class Ability : ICloneable
    {
        [System.Text.Json.Serialization.JsonPropertyName("abilityName")]
        public string? AbilityName { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("effect")]
        public string? Effect { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("description")]
        public string? Description { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("id")]
        public string? Id { get; set; }

        public Object Clone()
        {
            return new Ability
            {
                AbilityName = this.AbilityName,
                Effect = this.Effect,
                Description = this.Description,
                Id = this.Id
            };
        }
    }
}
