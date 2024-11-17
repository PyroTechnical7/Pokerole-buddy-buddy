namespace PokeroleBuddyHelper.Models;

using System.ComponentModel;
using System.Runtime.CompilerServices;

public class PokemonMove : INotifyPropertyChanged
{
    private string _name = string.Empty;
    private TrainerRank _learnedRank = TrainerRank.Starter;
    private int _learnedRankIndex;

    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }

    public TrainerRank LearnedRank
    {
        get => _learnedRank;
        set
        {
            if (_learnedRank != value)
            {
                _learnedRank = value;
                OnPropertyChanged();
            }
        }
    }

    public int LearnedRankIndex
    {
        get => (int) LearnedRank;
        set
        {
            LearnedRank = (TrainerRank)value;
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void UpdateLearnedRank()
    {
        OnPropertyChanged(nameof(LearnedRank));
    }

    public int GetEnumIndex(TrainerRank rank)
    {
        // Convert enum values to an array
        var values = Enum.GetValues(typeof(TrainerRank));

        // Find the index of the specified rank
        return Array.IndexOf(values, rank);
    }
}
