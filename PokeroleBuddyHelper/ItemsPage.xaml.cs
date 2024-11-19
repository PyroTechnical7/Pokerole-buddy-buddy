using PokeroleBuddyHelper.Models;
using PokeroleBuddyHelper.Services;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace PokeroleBuddyHelper;

public partial class ItemsPage : ContentPage
{
    private List<Item> _itemList = new List<Item>();
    private ObservableCollection<Item> _filteredItems = new();
    private readonly ItemService _itemService = new ItemService();
    public ICommand DeleteItemCommand { get; }
    public ItemsPage()
	{
		InitializeComponent();
        LoadItemData();
        DeleteItemCommand = new Command<Item>(OnDeleteItem);
    }

    private async void OnDeleteItem(Item item)
    {
        if (item != null && _itemList.Contains(item))
        {
            _itemList.Remove(item);
        }
        await SaveItemData();
    }
    private async Task SaveItemData()
    {
        _itemService.SaveItemsAsync(_itemList);
        _filteredItems = new ObservableCollection<Item>(_itemList);
        ItemListView.ItemsSource = _filteredItems;

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
            Name = "New Item"
        };
        _itemList.Add(newItem);
        await _itemService.SaveItemsAsync(_itemList);
        ItemListView.ItemsSource = null;
        ItemListView.ItemsSource = _itemList;

        await Navigation.PushAsync(new EditItemPage(newItem));
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

    private async void ImportWithExportClicked(object Sender, EventArgs e)
    {
        var exportCollection = new ItemCollection
        {
            ItemList = this._itemList.Select(p => (Item)p.Clone()).ToList()
        };

        var jsonFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.MacCatalyst, new[] { "public.json" } },
                { DevicePlatform.Android, new[] { "application/json" } },
                { DevicePlatform.WinUI, new[] { ".json" } },
                { DevicePlatform.Tizen, new[] { "application/json" } }
            });

        ItemCollection originalCollection = await PromptForFile(jsonFileType);

        if (originalCollection == null) return;

        exportCollection.ItemList.AddRange(originalCollection.ItemList);

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

    private async Task<ItemCollection> PromptForFile(FilePickerFileType jsonFileType)
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
                var importedItemCollection = JsonSerializer.Deserialize<ItemCollection>(json);

                // Debugging: Check if the deserialization was successful
                if (importedItemCollection == null)
                {
                    System.Diagnostics.Debug.WriteLine("Deserialization failed: importedItemCollection is null");
                    Title = "importedItemCollection is null";
                    return null;
                }
                else if (importedItemCollection.ItemList == null)
                {
                    Title = "ItemCollection is null";
                    System.Diagnostics.Debug.WriteLine("Deserialization failed: ItemCollection is null");
                }
                else return importedItemCollection;
            }
            catch (JsonException ex)
            {
                // Handle JSON deserialization error
                await DisplayAlert("Error", $"Failed to import Item data: {ex.Message}", "OK");
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

    public async void OnOrderAlphabetically(object sender, EventArgs e)
    {
        _itemList = _itemList.OrderBy(p => p.Name).ToList();
        ItemListView.ItemsSource = null;
        ItemListView.ItemsSource = _itemList;
    }
}