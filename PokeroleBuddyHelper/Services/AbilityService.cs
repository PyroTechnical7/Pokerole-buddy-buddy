using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using PokeroleBuddyHelper.Models;
using Microsoft.Maui.Storage;

namespace PokeroleBuddyHelper.Services
{
    public class AbilityService
    {
        private readonly string _filePath = Path.Combine(FileSystem.AppDataDirectory, "abilities.json");
        private readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

        public async Task<List<Ability>> LoadAbilitiesAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Ability>();
            }

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Ability>>(json) ?? new List<Ability>();
        }

        public async Task SaveAbilitiesAsync(List<Ability> abilityList)
        {
            var json = JsonSerializer.Serialize(abilityList, _jsonSerializerOptions);
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}