using PokeroleBuddyHelper.Models;
using PokeroleBuddyHelper.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Text.Json;
using System.Collections.ObjectModel;

namespace PokeroleBuddyHelper
{
    public partial class MainPage : ContentPage
    {
        private readonly PokemonService _pokemonService = new();
        private readonly ItemService _itemService = new();
        private readonly MoveService _moveService = new();
        private readonly AbilityService _abilityService = new();

        private List<Pokemon> _pokemonList = [];
        private ObservableCollection<Pokemon> _filteredPokemon = [];
        private List<Item> _itemList = [];
        private List<Move> _moveList = [];
        private List<PokemonAbility> _abilityList = [];

        public MainPage()
        {
            InitializeComponent();
            LoadPokemonData();
        }

        private async void LoadPokemonData()
        {
            _pokemonList = await _pokemonService.LoadPokemonAsync();
            _filteredPokemon = new ObservableCollection<Pokemon>(_pokemonList);
            PokemonListView.ItemsSource = _filteredPokemon;
        }
        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = e.NewTextValue.ToLower();
            var filteredList = _pokemonList.Where(p => p.Name.Contains(searchText, StringComparison.CurrentCultureIgnoreCase)).ToList();
            _filteredPokemon = new ObservableCollection<Pokemon>(filteredList);
            PokemonListView.ItemsSource = _filteredPokemon;
        }

        private async void OnAddPokemonClicked(object sender, EventArgs e)
        {
            var newPokemon = new Pokemon
            {
                Name = "New Pokémon",
                DexCategory = "Unknown",
                // Initialize other properties as needed
            };
            _pokemonList.Add(newPokemon);
            await _pokemonService.SavePokemonAsync(_pokemonList);
            PokemonListView.ItemsSource = null;
            PokemonListView.ItemsSource = _pokemonList;
        }

        private async void OnPokemonSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Pokemon selectedPokemon)
            {
                await Navigation.PushAsync(new EditPokemonPage(selectedPokemon));
            }

            PokemonListView.SelectedItem = null;
        }

        private async void OnImportPokemonClicked(object sender, EventArgs e)
        {
            var jsonFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.MacCatalyst, new[] { "public.json" } }, // or "json" if "public.json" doesn't work
                { DevicePlatform.Android, new[] { "application/json" } },
                { DevicePlatform.WinUI, new[] { ".json" } },
                { DevicePlatform.Tizen, new[] { "application/json" } }
            });
           PokemonCollectionWrapper pokemonCollectionWrapper = await PromptForFile(jsonFileType);

            _pokemonList = pokemonCollectionWrapper.PokemonCollection;
            PokemonListView.ItemsSource = null;
            PokemonListView.ItemsSource = _pokemonList;
        }

        private async Task<PokemonCollectionWrapper> PromptForFile(FilePickerFileType jsonFileType)
        {
            var result = await FilePicker.PickAsync();

            if (result != null)
            {
                Title = $"File Name: {result.FileName}";
                try
                {
                    using var stream = await result.OpenReadAsync();
                    using var reader = new StreamReader(stream);
                    var json = await reader.ReadToEndAsync();
                    var importedPokemonWrapper = JsonSerializer.Deserialize<PokemonCollectionWrapper>(json);

                    // Debugging: Check if the deserialization was successful
                    if (importedPokemonWrapper == null)
                    {
                        System.Diagnostics.Debug.WriteLine("Deserialization failed: importedPokemonWrapper is null");
                        Title = "importedPokemonWrapper is null";
                        return null;
                    }
                    else if (importedPokemonWrapper.PokemonCollection == null)
                    {
                        Title = "PokemonCollection is null";
                        System.Diagnostics.Debug.WriteLine("Deserialization failed: PokemonCollection is null");
                    }
                    else return importedPokemonWrapper;
                }
                catch (JsonException ex)
                {
                    // Handle JSON deserialization error
                    await DisplayAlert("Error", $"Failed to import Pokémon data: {ex.Message}", "OK");
                    return null;
                }
                catch (Exception ex)
                {
                    // Handle other potential errors
                    await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
                    return null;
                }
            }
            Title = "Result was null";
            return null;
        }

        public async void OnExportPokemonClicked(object sender, EventArgs e)
        {
            PokemonCollectionWrapper exportCollection = new()
            {
                PokemonCollection = this._pokemonList
            };

            try
            {
                var json = JsonSerializer.Serialize(exportCollection, new JsonSerializerOptions { WriteIndented = true });

                var fileName = $"PokemonExport_{DateTime.Now:yyyyMMddHHmmss}.json";
                var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);

                await File.WriteAllTextAsync(filePath, json);

                await DisplayAlert("Success", $"Pokémon data exported to {filePath}", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to export Pokémon data: {ex.Message}", "OK");
            }

        }

        private async void OnImportPokemonWithExportClicked(object sender, EventArgs e)
        {
            PokemonCollectionWrapper exportCollection = new()
            {
                PokemonCollection = this._pokemonList
            };

            var jsonFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.iOS, new[] { "public.json" } }, // or "json" if "public.json" doesn't work
                { DevicePlatform.Android, new[] { "application/json" } },
                { DevicePlatform.WinUI, new[] { ".json" } },
                { DevicePlatform.Tizen, new[] { "application/json" } }
            });

            var originalCollectionWrapper = await PromptForFile(jsonFileType);
            if (originalCollectionWrapper == null) return;

            var originalCollection = originalCollectionWrapper.PokemonCollection;

            foreach (var pokemon in _pokemonList)
            {
                var existingPokemon = originalCollection.FirstOrDefault(p => p._id == pokemon.EvolvesFrom);
                if (existingPokemon != null)
                {
                    Evolution customEvolution = new()
                    {
                        Kind = "Level",
                        To = pokemon._id,
                        Item = "",
                        Speed = 0
                    };
                    existingPokemon.Evolutions.Add(customEvolution);
                }
            }

            exportCollection.PokemonCollection.AddRange(originalCollection);

            try
            {
                var json = JsonSerializer.Serialize(exportCollection, new JsonSerializerOptions { WriteIndented = true });

                var fileName = $"PokemonExport_{DateTime.Now:yyyyMMddHHmmss}.json";
                var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);

                await File.WriteAllTextAsync(filePath, json);

                await DisplayAlert("Success", $"Pokémon data exported to {filePath}", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to export Pokémon data: {ex.Message}", "OK");
            }
        }

        public async void OnOrderAlphabetically(object sender, EventArgs e)
        {
            _pokemonList = _pokemonList.OrderBy(p => p.Name).ToList();
            PokemonListView.ItemsSource = null;
            PokemonListView.ItemsSource = _pokemonList;
        }

        public async void OnOrderNumerically(object sender, EventArgs e)
        {
            _pokemonList = _pokemonList.OrderBy(p => p.Number).ToList();
            PokemonListView.ItemsSource = null;
            PokemonListView.ItemsSource = _pokemonList;
        }

        public async void OnClearClicked(object sender, EventArgs e)
        {
            _pokemonList.Clear();
            await _pokemonService.SavePokemonAsync(_pokemonList);
            PokemonListView.ItemsSource = null;
            PokemonListView.ItemsSource = _pokemonList;
        }

    }
}