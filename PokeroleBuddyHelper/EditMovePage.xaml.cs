using PokeroleBuddyHelper.Models;
using System.Collections.ObjectModel;

namespace PokeroleBuddyHelper;

public partial class EditMovePage : ContentPage
{
	private Move _move;
    private readonly Move unedittedMove;
    public ObservableCollection<PokemonType> Types
    {
        get; set;
    }

    public Move Move
    {
        get { return _move; }
        set { _move = value; OnPropertyChanged(nameof(Move)); }
    }
    public EditMovePage(Move move)
	{
		InitializeComponent();
        Types = new ObservableCollection<PokemonType>(Enum.GetValues(typeof(PokemonType)).Cast<PokemonType>());
        this._move = move;
        unedittedMove = (Move) move.Clone();
        BindingContext = this; 
    }

	private async void OnSaveClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        Move = unedittedMove;
    }
}