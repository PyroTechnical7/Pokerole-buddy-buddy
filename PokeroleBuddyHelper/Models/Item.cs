using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleBuddyHelper.Models
{
    public class Item: ICloneable
    {
        public string? Name { get; set; }
        public string? ID { get; set; }
        public string? Source { get; set; }
        public bool PMD { get; set; }
        public string? Pocket { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public bool OneUse { get; set; }
        public int TrainerPrice { get; set; }
        public int HealthRestored { get; set; }
        public string? Cures { get; set; }
        public List<string>? Boost { get; set; }
        public int Value { get; set; }
        public string? ForTypes { get; set; }
        public List<String>? ForPokemon { get; set; }
        public string? ItemSpritePath { get; set; }
        public object Clone()
        {
            return new Item
            {
                Name = this.Name,
                ID = this.ID,
                Source = this.Source,
                PMD = this.PMD,
                Pocket = this.Pocket,
                Category = this.Category,
                Description = this.Description,
                OneUse = this.OneUse,
                TrainerPrice = this.TrainerPrice,
                HealthRestored = this.HealthRestored,
                Cures = this.Cures,
                Boost = this.Boost != null ? new List<string>(this.Boost) : null,
                Value = this.Value,
                ForTypes = this.ForTypes,
                ForPokemon = this.ForPokemon != null ? new List<string>(this.ForPokemon) : null,
                ItemSpritePath = this.ItemSpritePath
            };
        }
    }
}
