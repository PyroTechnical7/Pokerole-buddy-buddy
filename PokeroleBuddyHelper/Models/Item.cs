using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleBuddyHelper.Models
{
    public class Item: ICloneable
    {
        private string? _name;
        private string? _id;
        private string? _source;
        private bool _pmd;
        private string? _pocket;
        private string? _category;
        private string? _description;
        private bool _oneUse;
        private int _trainerPrice;
        private int _healthRestored;
        private string? _cures;
        private List<string>? _boost;
        private int _value;
        private string? _forTypes;
        private List<string>? _forPokemon;
        private string? _itemSpritePath;

        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string? ID
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string? Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

        public bool PMD
        {
            get => _pmd;
            set => SetProperty(ref _pmd, value);
        }

        public string? Pocket
        {
            get => _pocket;
            set => SetProperty(ref _pocket, value);
        }

        public string? Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public string? Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public bool OneUse
        {
            get => _oneUse;
            set => SetProperty(ref _oneUse, value);
        }

        public int TrainerPrice
        {
            get => _trainerPrice;
            set => SetProperty(ref _trainerPrice, value);
        }

        public int HealthRestored
        {
            get => _healthRestored;
            set => SetProperty(ref _healthRestored, value);
        }

        public string? Cures
        {
            get => _cures;
            set => SetProperty(ref _cures, value);
        }

        public List<string>? Boost
        {
            get => _boost;
            set => SetProperty(ref _boost, value);
        }

        public int Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public string? ForTypes
        {
            get => _forTypes;
            set => SetProperty(ref _forTypes, value);
        }

        public List<string>? ForPokemon
        {
            get => _forPokemon;
            set => SetProperty(ref _forPokemon, value);
        }

        public string? ItemSpritePath
        {
            get => _itemSpritePath;
            set => SetProperty(ref _itemSpritePath, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public object Clone()
        {
            return new Item
            {
                Name = this.Name,
                ID = this.ID,
                Source = this.Source,
                PMD = this.PMD,
                Pocket = this.Pocket,
                Category = this.Category,
                Description = this.Description,
                OneUse = this.OneUse,
                TrainerPrice = this.TrainerPrice,
                HealthRestored = this.HealthRestored,
                Cures = this.Cures,
                Boost = this.Boost != null ? new List<string>(this.Boost) : null,
                Value = this.Value,
                ForTypes = this.ForTypes,
                ForPokemon = this.ForPokemon != null ? new List<string>(this.ForPokemon) : null,
                ItemSpritePath = this.ItemSpritePath
            };
        }
    }
}
