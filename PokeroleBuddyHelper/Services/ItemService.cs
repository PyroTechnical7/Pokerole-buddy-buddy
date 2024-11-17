using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using PokeroleBuddyHelper.Models;
using Microsoft.Maui.Storage;

namespace PokeroleBuddyHelper.Services
{
    public class ItemService
    {
        private readonly string _filePath = Path.Combine(FileSystem.AppDataDirectory, "items.json");

        public async Task<List<Item>> LoadItemsAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Item>();
            }

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Item>>(json) ?? [];
        }

        public async Task SaveItemsAsync(List<Item> itemList)
        {
            var json = JsonSerializer.Serialize(itemList, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }

        public bool IsItemsEmpty()
        {
            return !File.Exists(_filePath);
        }
    }
}