using PokeroleBuddyHelper.Models;
using PokeroleBuddyHelper.Services;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace PokeroleBuddyHelper;

public partial class MovesPage : ContentPage
{
    private readonly MoveService _moveService = new();
    private List<Move> _moveList = new();
    private ObservableCollection<Move> _filteredMoves = new();

    public ICommand DeleteMoveCommand { get; }

    public MovesPage()
	{
		InitializeComponent();
        LoadMoveData();
        DeleteMoveCommand = new Command<Move>(OnDeleteMove);
    }


    private async void OnDeleteMove(Move move)
    {
        if (move != null && _moveList.Contains(move))
        {
            _moveList.Remove(move);
        }
        await SaveMoveData();
    }

    private async Task SaveMoveData()
    {
        await _moveService.SaveMovesAsync(_moveList);
        _filteredMoves = new ObservableCollection<Move>(_moveList);
        MoveListView.ItemsSource = _filteredMoves;
    }

    private async void LoadMoveData()
    {
        _moveList = await _moveService.LoadMovesAsync();
        _filteredMoves = new(_moveList);
        MoveListView.ItemsSource = _moveList;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _moveService.SaveMovesAsync(_moveList);
    }

    private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = e.NewTextValue.ToLower();
        _filteredMoves = new(_moveList.Where(p => p.Name.Contains(searchText, StringComparison.CurrentCultureIgnoreCase)).ToList());
        MoveListView.ItemsSource = _filteredMoves;
    }

    private async void OnClearClicked(object sender, EventArgs e)
    {
        _moveList.Clear();
        await _moveService.SaveMovesAsync(_moveList);
        MoveListView.ItemsSource = null;
        MoveListView.ItemsSource = _moveList;
    }

    private async void OnAddMoveClicked(object sender, EventArgs e)
    {
        var newMove = new Move
        {
            Name = "New Move",
            // Initialize other properties as needed
        };
        _moveList.Add(newMove);
        await _moveService.SaveMovesAsync(_moveList);
        MoveListView.ItemsSource = null;
        MoveListView.ItemsSource = _moveList;

        await Navigation.PushAsync(new EditMovePage(newMove));
    }


    private async void OnMoveSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Move selectedMove)
        {
            await Navigation.PushAsync(new EditMovePage(selectedMove));
        }

        MoveListView.SelectedItem = null;
    }

    private async void OnImportMovesClicked(object sender, EventArgs e)
    {
        var jsonFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.MacCatalyst, new[] { "public.json" } },
                { DevicePlatform.Android, new[] { "application/json" } },
                { DevicePlatform.WinUI, new[] { ".json" } },
                { DevicePlatform.Tizen, new[] { "application/json" } }
            });

        var result = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = jsonFileType,
            PickerTitle = "Select a Moves JSON file"
        });

        if (result != null)
        {
            try
            {
                using var stream = await result.OpenReadAsync();
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();
                _moveList = JsonSerializer.Deserialize<MoveCollection>(json).Moves;

                // Debugging: Check if the deserialization was successful
                if (_moveList == null)
                {
                    System.Diagnostics.Debug.WriteLine("Deserialization failed: _moveList is null");
                }
                else
                {
                    await _moveService.SaveMovesAsync(_moveList);
                    MoveListView.ItemsSource = null;
                    MoveListView.ItemsSource = _moveList;
                }
            }
            catch (JsonException ex)
            {
                // Handle JSON deserialization error
                await DisplayAlert("Error", $"Failed to import moves data: {ex.Message}", "OK");
            }
            catch (Exception ex)
            {
                // Handle other potential errors
                await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
            }
        }
    }

    private async void ImportWithExportClicked(object Sender, EventArgs e)
    {
        var exportCollection = new MoveCollection
        {
            Moves = _moveList.Select(p => (Move) p.Clone()).ToList()
        };

        var jsonFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.MacCatalyst, new[] { "public.json" } },
                { DevicePlatform.Android, new[] { "application/json" } },
                { DevicePlatform.WinUI, new[] { ".json" } },
                { DevicePlatform.Tizen, new[] { "application/json" } }
            });

        MoveCollection originalCollection = await PromptForFile(jsonFileType);

        if (originalCollection == null) return;

        exportCollection.Moves.AddRange(originalCollection.Moves);

        try
        {
            var json = JsonSerializer.Serialize(exportCollection, new JsonSerializerOptions { WriteIndented = true });

            var fileName = $"MovesExport_{DateTime.Now:yyyyMMddHHmmss}.json";
            var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);

            await File.WriteAllTextAsync(filePath, json);

            await DisplayAlert("Success", $"Move data exported to {filePath}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to export Move data: {ex.Message}", "OK");
        }

    }

    private async Task<MoveCollection> PromptForFile(FilePickerFileType jsonFileType)
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
                var importedMoveCollection = JsonSerializer.Deserialize<MoveCollection>(json);

                // Debugging: Check if the deserialization was successful
                if (importedMoveCollection == null)
                {
                    System.Diagnostics.Debug.WriteLine("Deserialization failed: importedMoveCollection is null");
                    Title = "importedMoveCollection is null";
                    return null;
                }
                else if (importedMoveCollection.Moves == null)
                {
                    Title = "MoveCollection is null";
                    System.Diagnostics.Debug.WriteLine("Deserialization failed: MoveCollection is null");
                }
                else return importedMoveCollection;
            }
            catch (JsonException ex)
            {
                // Handle JSON deserialization error
                await DisplayAlert("Error", $"Failed to import MoveCollection data: {ex.Message}", "OK");
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

    public async void OnExportMovesClicked(object sender, EventArgs e)
    {
        MoveCollection exportCollection = new()
        {
            Moves = this._moveList
        };

        try
        {
            var json = JsonSerializer.Serialize(exportCollection, new JsonSerializerOptions { WriteIndented = true });

            var fileName = $"MovesExport_{DateTime.Now:yyyyMMddHHmmss}.json";
            var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);

            await File.WriteAllTextAsync(filePath, json);

            await DisplayAlert("Success", $"Move data exported to {filePath}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to export Move data: {ex.Message}", "OK");
        }

    }

    public async void OnOrderAlphabetically(object sender, EventArgs e)
    {
        _moveList = _moveList.OrderBy(p => p.Name).ToList();
        MoveListView.ItemsSource = null;
        MoveListView.ItemsSource = _moveList;
    }
}