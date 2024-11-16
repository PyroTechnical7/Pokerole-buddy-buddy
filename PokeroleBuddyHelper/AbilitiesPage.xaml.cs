using PokeroleBuddyHelper.Models;
using PokeroleBuddyHelper.Services;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace PokeroleBuddyHelper;

public partial class AbilitiesPage : ContentPage
{
    private List<Ability> _abilityList = new List<Ability>();
    private ObservableCollection<Ability> _filteredAbilityList = new();
    private readonly AbilityService _abilityService = new AbilityService();

    public ICommand DeleteAbilityCommand { get; }

    public AbilitiesPage()
	{
		InitializeComponent();
        LoadAbilityData();
        DeleteAbilityCommand = new Command<Ability>(OnDeleteAbility);
    }

    private async void OnDeleteAbility(Ability ability)
    {
        if (ability != null && _abilityList.Contains(ability))
        {
            _abilityList.Remove(ability);
        }
        await SaveAbilityData();
    }

    private async Task SaveAbilityData()
    {
        await _abilityService.SaveAbilitiesAsync(_abilityList);
        _filteredAbilityList = new ObservableCollection<Ability>(_abilityList);
        AbilityListView.ItemsSource = _filteredAbilityList;

    }


    private async void LoadAbilityData()
    {
        _abilityList = await _abilityService.LoadAbilitiesAsync();
        _filteredAbilityList = new(_abilityList);
        AbilityListView.ItemsSource = _filteredAbilityList;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _abilityService.SaveAbilitiesAsync(_abilityList);
    }

    private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = e.NewTextValue.ToLower();

        _filteredAbilityList = new(_abilityList.Where(p => p.AbilityName.Contains(searchText, StringComparison.CurrentCultureIgnoreCase)).ToList());
        AbilityListView.ItemsSource = _filteredAbilityList;
    }

    private async void OnAddAbilityClicked(object sender, EventArgs e)
    {
        var newAbility = new Ability
        {
            AbilityName = "New PokemonAbility",
            // Initialize other properties as needed
        };
        _abilityList.Add(newAbility);
        await _abilityService.SaveAbilitiesAsync(_abilityList);
        AbilityListView.ItemsSource = null;
        AbilityListView.ItemsSource = _abilityList;
    }

    private async void OnAbilitySelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Ability selectedAbility)
        {
            await Navigation.PushAsync(new EditAbilityPage(selectedAbility));
        }

        AbilityListView.SelectedItem = null;
    }

    private async void OnImportAbilitiesClicked(object sender, EventArgs e)
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
            PickerTitle = "Select an Abilities JSON file"
        });

        if (result != null)
        {
            try
            {
                using var stream = await result.OpenReadAsync();
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();
                _abilityList = JsonSerializer.Deserialize<AbilityCollection>(json).Abilities;

                // Debugging: Check if the deserialization was successful
                if (_abilityList == null)
                {
                    System.Diagnostics.Debug.WriteLine("Deserialization failed: _abilityList is null");
                }
                else
                {
                    await _abilityService.SaveAbilitiesAsync(_abilityList);
                    AbilityListView.ItemsSource = null;
                    AbilityListView.ItemsSource = _abilityList;
                }
            }
            catch (JsonException ex)
            {
                // Handle JSON deserialization error
                await DisplayAlert("Error", $"Failed to import abilities data: {ex.Message}", "OK");
            }
            catch (Exception ex)
            {
                // Handle other potential errors
                await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
            }
        }
    }

    public async void OnExportAbilitiesClicked(object sender, EventArgs e)
    {
        AbilityCollection exportCollection = new()
        {
            Abilities = this._abilityList
        };

        try
        {
            var json = JsonSerializer.Serialize(exportCollection, new JsonSerializerOptions { WriteIndented = true });

            var fileName = $"AbilitiesExport_{DateTime.Now:yyyyMMddHHmmss}.json";
            var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);

            await File.WriteAllTextAsync(filePath, json);

            await DisplayAlert("Success", $"Ability data exported to {filePath}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to export Ability data: {ex.Message}", "OK");
        }

    }

    public async void OnClearClicked(object sender, EventArgs e)
    {
        _abilityList.Clear();
        await _abilityService.SaveAbilitiesAsync(_abilityList);
        AbilityListView.ItemsSource = null;
        AbilityListView.ItemsSource = _abilityList;
    }
}