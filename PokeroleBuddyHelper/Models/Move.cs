using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleBuddyHelper.Models
{
    public class Move : ICloneable
    {
        private string? _name = string.Empty;
        private PokemonType? _type = PokemonType.Normal;
        private int _power;
        private string? _damage1 = string.Empty;
        private string? _damage2 = string.Empty;
        private string? _accuracy1 = string.Empty;
        private string? _accuracy2 = string.Empty;
        private string? _target = string.Empty;
        private string? _effect = string.Empty;
        private string? _description = string.Empty;
        private string? _id = string.Empty;
        private string? _category = string.Empty;

        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public PokemonType? Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public int Power
        {
            get => _power;
            set => SetProperty(ref _power, value);
        }

        public string? Damage1
        {
            get => _damage1;
            set => SetProperty(ref _damage1, value);
        }

        public string? Damage2
        {
            get => _damage2;
            set => SetProperty(ref _damage2, value);
        }

        public string? Accuracy1
        {
            get => _accuracy1;
            set => SetProperty(ref _accuracy1, value);
        }

        public string? Accuracy2
        {
            get => _accuracy2;
            set => SetProperty(ref _accuracy2, value);
        }

        public string? Target
        {
            get => _target;
            set => SetProperty(ref _target, value);
        }

        public string? Effect
        {
            get => _effect;
            set => SetProperty(ref _effect, value);
        }

        public string? Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string? Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string? Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public object Clone()
        {
            return new Move
            {
                Name = this.Name,
                Type = this.Type,
                Power = this.Power,
                Damage1 = this.Damage1,
                Damage2 = this.Damage2,
                Accuracy1 = this.Accuracy1,
                Accuracy2 = this.Accuracy2,
                Target = this.Target,
                Effect = this.Effect,
                Description = this.Description,
                Id = this.Id,
                Category = this.Category
            };
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
