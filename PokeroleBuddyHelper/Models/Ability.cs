using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleBuddyHelper.Models
{
    public class Ability : ICloneable
    {
        private string? _abilityName;
        private string? _effect;
        private string? _description;
        private string? _id;

        [System.Text.Json.Serialization.JsonPropertyName("abilityName")]
        public string? AbilityName {
            get => _abilityName;
            set =>
                SetProperty(ref _abilityName, value);
            
        }
        [System.Text.Json.Serialization.JsonPropertyName("effect")]
        public string? Effect { 
            get => _effect;
            set => SetProperty(ref _effect, value);
        }

        [System.Text.Json.Serialization.JsonPropertyName("description")]
        public string? Description { 
            get => _description; 
            set => SetProperty(ref _description, value);
        }
        [System.Text.Json.Serialization.JsonPropertyName("id")]
        public string? Id { 
            get => _id; 
            set => SetProperty(ref _id, value); 
        }

        public Object Clone()
        {
            return new Ability
            {
                AbilityName = this.AbilityName,
                Effect = this.Effect,
                Description = this.Description,
                Id = this.Id
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
    }
}
