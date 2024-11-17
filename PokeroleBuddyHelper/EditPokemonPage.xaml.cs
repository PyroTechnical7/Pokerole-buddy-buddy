using Microsoft.Maui.Controls;
using PokeroleBuddyHelper.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace PokeroleBuddyHelper
{
    public partial class EditPokemonPage : ContentPage
    {
        private readonly string _spriteString = "Graphics/PokemonSprites/regular/";
        private readonly string _shinySpriteString = "Graphics/PokemonSprites/shiny/";

        private Pokemon _pokemon;
        private readonly Pokemon unedittedPokemon;
        private bool _autoConvertWeightEnabled  { get; set; }
        private bool _autoConvertHeightEnabled { get; set; }
        public ICommand DeleteMoveCommand { get; }
        public ICommand DeleteEvolutionCommand { get; }

        // Properties for original stats
        public int OriginalHP { get; set; }
        public int OriginalAttack { get; set; }
        public int OriginalDefense { get; set; }
        public int OriginalSpecialAttack { get; set; }
        public int OriginalSpecialDefense { get; set; }
        public int OriginalSpeed { get; set; }

        public ObservableCollection<PokemonType> Types {
            get; set;
        }

        public ObservableCollection<GenderType> GenderTypes
        {
            get; set;
        }

        public ObservableCollection<TrainerRank> TrainerRanks
        {
            get; set;
        }

        public bool AutoConvertWeightEnabled
        {
            get { return _autoConvertWeightEnabled; }
            set { _autoConvertWeightEnabled = value; OnPropertyChanged(nameof(AutoConvertWeightEnabled)); }
        }

        public bool AutoConvertHeightEnabled
        {
            get { return _autoConvertHeightEnabled; }
            set { _autoConvertHeightEnabled = value; OnPropertyChanged(nameof(AutoConvertHeightEnabled)); }
        }

        public Pokemon Pokemon
        {
            get { return _pokemon; }
            set { _pokemon = value; OnPropertyChanged(nameof(Pokemon)); }
        }

        public EditPokemonPage(Pokemon pokemon)
        {
            _autoConvertHeightEnabled = false;
            _autoConvertWeightEnabled = false;
            _pokemon = pokemon;
            unedittedPokemon = (Pokemon) pokemon.Clone();

            // Initialize the Types collection with possible values
            Types = new ObservableCollection<PokemonType>(Enum.GetValues(typeof(PokemonType)).Cast<PokemonType>());
            GenderTypes = new ObservableCollection<GenderType>(Enum.GetValues(typeof(GenderType)).Cast<GenderType>());
            TrainerRanks = new ObservableCollection<TrainerRank>(Enum.GetValues(typeof(TrainerRank)).Cast<TrainerRank>());

            DeleteMoveCommand = new Command<PokemonMove>(OnDeleteMove);
            DeleteEvolutionCommand = new Command<Evolution>(OnRemoveEvolution);
            BindingContext = this;
            InitializeComponent();

        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Save changes and navigate back
            await Navigation.PopAsync();
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            Pokemon = (Pokemon)unedittedPokemon.Clone();

        }

        private async void OnAddMoveClicked(object sender, EventArgs e)
        {
            var newMove = new PokemonMove { Name = "New Move", LearnedRank = TrainerRank.Starter };
            Pokemon.Moves.Add(newMove);
            newMove.UpdateLearnedRank();
        }

        private void OnAddEvolutionClicked(object sender, EventArgs e)
        {
            Pokemon.Evolutions.Add(new Evolution());
        }

        private void OnRemoveEvolution(Evolution evolution)
        {
            if (Pokemon.Evolutions.Contains(evolution))
            {
                Pokemon.Evolutions.Remove(evolution);
            }
        }

        private void OnDeleteMove(PokemonMove move)
        {
            var pokemon = ((EditPokemonPage) BindingContext).Pokemon;
            pokemon.Moves.Remove(move);
        }

        private void OnWeightKgChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(e.NewTextValue, out double kg) && _autoConvertWeightEnabled)
            {
                double lbs = Math.Round(kg * 2.20462, 3);
                WeightLbsEntry.TextChanged -= OnWeightLbsChanged; // Unsubscribe to avoid recursive call
                WeightLbsEntry.Text = lbs.ToString();
                WeightLbsEntry.TextChanged += OnWeightLbsChanged; // Resubscribe
            }
        }

        private void OnWeightLbsChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(e.NewTextValue, out double lbs) && _autoConvertWeightEnabled)
            {
                double kg = Math.Round(lbs / 2.20462, 3);
                WeightKgEntry.TextChanged -= OnWeightKgChanged; // Unsubscribe to avoid recursive call
                WeightKgEntry.Text = kg.ToString();
                WeightKgEntry.TextChanged += OnWeightKgChanged; // Resubscribe
            }
        }

        private void OnHeightMChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(e.NewTextValue, out double meters) && _autoConvertHeightEnabled)
            {
                double feet = Math.Round(meters * 3.28084, 3);
                HeightFtEntry.TextChanged -= OnHeightFtChanged; // Unsubscribe to avoid recursive call
                HeightFtEntry.Text = feet.ToString();
                HeightFtEntry.TextChanged += OnHeightFtChanged; // Resubscribe
            }
        }

        private void OnHeightFtChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(e.NewTextValue, out double feet) && _autoConvertHeightEnabled)
            {
                double meters = Math.Round(feet / 3.28084, 3);
                HeightMEntry.TextChanged -= OnHeightMChanged; // Unsubscribe to avoid recursive call
                HeightMEntry.Text = meters.ToString();
                HeightMEntry.TextChanged += OnHeightMChanged; // Resubscribe
            }
        }

        private void OnConvertStatsClicked(object sender, EventArgs e)
        {
            // Convert and set the stats using the convertOriginalStat method
            (Pokemon.MinStrength, Pokemon.MaxStrength) = convertOriginalStat(OriginalAttack);
            (Pokemon.MinDexterity, Pokemon.MaxDexterity) = convertOriginalStat(OriginalSpeed);
            (Pokemon.MinVitality, Pokemon.MaxVitality) = convertOriginalStat(OriginalDefense);
            (Pokemon.MinSpecial, Pokemon.MaxSpecial) = convertOriginalStat(OriginalSpecialAttack);
            (Pokemon.MinInsight, Pokemon.MaxInsight) = convertOriginalStat(OriginalSpecialDefense);
        }

        private (int min, int max) convertOriginalStat(int ogStat)
        {
            switch (ogStat)
            {
                case < 20:
                    return (1, 2);
                case < 45:
                    return (1, 3);
                case < 70:
                    return (2, 4);
                case < 95:
                    return (2, 5);
                case < 120:
                    return (3, 6);
                case < 145:
                    return (3, 7);
                case < 170:
                    return (4, 8);
                case < 195:
                    return (4, 9);
                default:
                    return (5, 10);
            }
        }

        private void OnAutoFillSpritesNonUniqueClicked(object sender, EventArgs e)
        {
            Pokemon.BoxSprite = _spriteString + Pokemon._id + ".png";
            Pokemon.BoxSpriteFemale = _spriteString + Pokemon._id + ".png";
            Pokemon.BoxSpriteShiny = _spriteString + Pokemon._id + ".png";
            Pokemon.BoxSpriteFemaleShiny = _spriteString + Pokemon._id + ".png";
        }

        private void OnAutoFillSpritesWithUniqueGenderClicked(object sender, EventArgs e)
        {
            Pokemon.BoxSprite = _spriteString + Pokemon._id + ".png";
            Pokemon.BoxSpriteFemale = _spriteString + "female/" + Pokemon._id + ".png";
            Pokemon.BoxSpriteShiny = _spriteString + Pokemon._id + ".png";
            Pokemon.BoxSpriteFemaleShiny = _spriteString + "female/" + Pokemon._id + ".png";
        }

        private void OnAutoFillSpritesNonUniqueWithShinyClicked(object sender, EventArgs e)
        {
            Pokemon.BoxSprite = _spriteString + Pokemon._id + ".png";
            Pokemon.BoxSpriteFemale = _spriteString + Pokemon._id + ".png";
            Pokemon.BoxSpriteShiny = _shinySpriteString + Pokemon._id + ".png";
            Pokemon.BoxSpriteFemaleShiny = _shinySpriteString + Pokemon._id + ".png";
        }

        private void OnAutoFillSpritesWithUniqueGenderWithShinyClicked(object sender, EventArgs e)
        {
            Pokemon.BoxSprite = _spriteString + Pokemon._id + ".png";
            Pokemon.BoxSpriteFemale = _spriteString + "female/" + Pokemon._id + ".png";
            Pokemon.BoxSpriteShiny = _shinySpriteString + Pokemon._id + ".png";
            Pokemon.BoxSpriteFemaleShiny = _shinySpriteString + "female/" + Pokemon._id + ".png";
        }
    }
}