using PokeroleBuddyHelper.Models;
using PokeroleBuddyHelper.Services;

namespace PokeroleBuddyHelper
{
    public class DataService
    {
        private MoveService MoveService { get; set; }
        private PokemonService PokemonService { get; set; }
        private ItemService ItemService { get; set; }
        private AbilityService AbilityService { get; set; }

        public DataService()
        {
            MoveService = new MoveService();
            PokemonService = new PokemonService();
            ItemService = new ItemService();
            AbilityService = new AbilityService();
        }

        public async void SaveItems(List<Item> items)
        {
            await ItemService.SaveItemsAsync(items);
        }

        public async void SaveMoves(List<Move> moves)
        {
            await MoveService.SaveMovesAsync(moves);
        }

        public async void SavePokemon(List<Pokemon> pokemon)
        {
            await PokemonService.SavePokemonAsync(pokemon);
        }

        public async void SaveAbilities(List<Ability> abilities)
        {
            await AbilityService.SaveAbilitiesAsync(abilities);
        }

        public async Task<List<Item>> Items()
        {
            return await ItemService.LoadItemsAsync();
        }

        public async Task<List<Move>> Moves()
        {
            return await MoveService.LoadMovesAsync();
        }

        public async Task<List<Pokemon>> Pokemon()
        {
            return await PokemonService.LoadPokemonAsync();
        }

        public async Task<List<Ability>> Abilities()
        {
            return await AbilityService.LoadAbilitiesAsync();
        }

        public async Task<List<PokemonMove>> PokemonMoves()
        {
            return await MoveService.LoadPokemonMovesAsync();
        }

    }
}