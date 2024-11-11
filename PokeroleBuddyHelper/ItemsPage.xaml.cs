using PokeroleBuddyHelper.Models;
using PokeroleBuddyHelper.Services;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace PokeroleBuddyHelper;

public partial class ItemsPage : ContentPage
{
    private List<Item> _itemList = new List<Item>();
    private ObservableCollection<Item> _filteredItems = new();
    private readonly ItemService _itemService = new ItemService();
    public ItemsPage()
	{
		InitializeComponent();
        LoadItemData();
    }

    private async void LoadItemData()
    {
        _itemList = await _itemService.LoadItemsAsync();
        _filteredItems = new(_itemList);
        ItemListView.ItemsSource = _filteredItems;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _itemService.SaveItemsAsync(_itemList);
    }

    private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = e.NewTextValue.ToLower();
        _filteredItems = new ObservableCollection<Item>(_itemList.Where(p => p.Name.Contains(searchText, StringComparison.CurrentCultureIgnoreCase)).ToList());
        ItemListView.ItemsSource = _filteredItems;
    }

    private async void OnAddItemClicked(object sender, EventArgs e)
    {
        var newItem = new Item
        {
            Name = "New Item",
            ID = Guid.NewGuid().ToString(),
            // Initialize other properties as needed
        };
        _itemList.Add(newItem);
        await _itemService.SaveItemsAsync(_itemList);
        ItemListView.ItemsSource = null;
        ItemListView.ItemsSource = _itemList;
    }

    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Item selectedItem)
        {
            await Navigation.PushAsync(new EditItemPage(selectedItem));
        }

        ItemListView.SelectedItem = null;
        await _itemService.SaveItemsAsync(_itemList);
    }

    private async void OnImportItemsClicked(object sender, EventArgs e)
    {
        var jsonFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.MacCatalyst, new[] { "public.json" } }, // or "json" if "public.json" doesn't work
                { DevicePlatform.Android, new[] { "application/json" } },
                { DevicePlatform.WinUI, new[] { ".json" } },
                { DevicePlatform.Tizen, new[] { "application/json" } }
            });

        var result = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = jsonFileType,
            PickerTitle = "Select an Items JSON file"
        });

        if (result != null)
        {
            try
            {
                using var stream = await result.OpenReadAsync();
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();
                _itemList = JsonSerializer.Deserialize<ItemCollection>(json).ItemList;

                // Debugging: Check if the deserialization was successful
                if (_itemList == null)
                {
                    System.Diagnostics.Debug.WriteLine("Deserialization failed: importedPokemonWrapper is null");
                }
                else
                {
                    await _itemService.SaveItemsAsync(_itemList);
                    ItemListView.ItemsSource = null;
                    ItemListView.ItemsSource = _itemList;
                }
            }
            catch (JsonException ex)
            {
                // Handle JSON deserialization error
                await DisplayAlert("Error", $"Failed to import Pokémon data: {ex.Message}", "OK");
            }
            catch (Exception ex)
            {
                // Handle other potential errors
                await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
            }
        }
    }

    public async void OnExportItemsClicked(object sender, EventArgs e)
    {
        ItemCollection exportCollection = new()
        {
            ItemList = this._itemList
        };

        try
        {
            var json = JsonSerializer.Serialize(exportCollection, new JsonSerializerOptions { WriteIndented = true });

            var fileName = $"ItemsExport_{DateTime.Now:yyyyMMddHHmmss}.json";
            var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);

            await File.WriteAllTextAsync(filePath, json);

            await DisplayAlert("Success", $"Item data exported to {filePath}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to export Item data: {ex.Message}", "OK");
        }

    }

    public async void OnClearClicked(object sender, EventArgs e)
    {
        _itemList.Clear();
        await _itemService.SaveItemsAsync(_itemList);
        ItemListView.ItemsSource = null;
        ItemListView.ItemsSource = _itemList;
    }
}