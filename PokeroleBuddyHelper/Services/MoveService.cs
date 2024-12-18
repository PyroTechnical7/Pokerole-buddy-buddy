﻿using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using PokeroleBuddyHelper.Models;
using Microsoft.Maui.Storage;

namespace PokeroleBuddyHelper.Services
{
    public class MoveService
    {
        private readonly string _filePath = Path.Combine(FileSystem.AppDataDirectory, "moves.json");

        public async Task<List<Move>> LoadMovesAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Move>();
            }

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Move>>(json) ?? [];
        }

        public async Task SaveMovesAsync(List<Move> moveList)
        {
            var json = JsonSerializer.Serialize(moveList, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}
