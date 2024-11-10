using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleBuddyHelper.Models
{
    public class Move : ICloneable
    {
        public string? Name { get; set; }
        public PokemonType? Type { get; set; }
        public int Power { get; set; }
        public string? Damage1 { get; set; }
        public string? Damage2 { get; set; }
        public string? Accuracy1 { get; set; }
        public string? Accuracy2 { get; set; }
        public string? Target { get; set; }
        public string? Effect { get; set; }
        public string? Description { get; set; }
        public string? Id { get; set; }
        public string? Category { get; set; }

        public object Clone()
        {
            return new Move
            {
                Name = this.Name,
                Type = this.Type,
                Power = this.Power,
                Damage1 = this.Damage1,
                Damage2 = this.Damage2,
                Accuracy1 = this.Accuracy1,
                Accuracy2 = this.Accuracy2,
                Target = this.Target,
                Effect = this.Effect,
                Description = this.Description,
                Id = this.Id,
                Category = this.Category
            };
        }
    }
}
