using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using PokeroleBuddyHelper.Models;
using Microsoft.Maui.Storage;

namespace PokeroleBuddyHelper.Services
{
    public class PokemonService
    {
        private readonly string _filePath = Path.Combine(FileSystem.AppDataDirectory, "pokemon.json");

        public async Task<List<Pokemon>> LoadPokemonAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Pokemon>();
            }

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Pokemon>>(json) ?? [];
        }

        public async Task SavePokemonAsync(List<Pokemon> pokemonList)
        {
            var json = JsonSerializer.Serialize(pokemonList, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}