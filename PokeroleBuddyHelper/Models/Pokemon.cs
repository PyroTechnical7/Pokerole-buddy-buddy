using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PokeroleBuddyHelper.Models;

public class Pokemon : INotifyPropertyChanged, ICloneable
{
    private int _minStrength;
    private int _maxStrength;
    private int _minDexterity;
    private int _maxDexterity;
    private int _minVitality;
    private int _maxVitality;
    private int _minSpecial;
    private int _maxSpecial;
    private int _minInsight;
    private int _maxInsight;
    private ObservableCollection<PokemonMove>? _moves;
    private int _number;
    private string? _dexID;
    private string? _name;
    private PokemonType _type1;
    private PokemonType _type2;
    private int _baseHP;
    private PokemonAbility? _ability1;
    private PokemonAbility? _ability2;
    private PokemonAbility? _hiddenAbility;
    private string? _eventAbilities;
    private TrainerRank? _recommendedRank;
    private GenderType? _genderType;
    private bool? _legendary;
    private bool? _goodStarter;
    private string? __id;
    private string? _dexCategory;
    private string? _dexDescription;
    private string? _boxSprite;
    private string? _boxSpriteShiny;
    private string? _boxSpriteFemale;
    private string? _boxSpriteFemaleShiny;
    private Height? _height;
    private Weight? _weight;
    private string? _evolvesFrom;
    private ObservableCollection<Evolution>? _evolutions;

    public int Number
    {
        get => _number;
        set
        {
            if (_number != value)
            {
                _number = value;
                OnPropertyChanged();
            }
        }
    }
    public string? DexID
    {
        get => _dexID;
        set
        {
            if (_dexID != value)
            {
                _dexID = value;
                OnPropertyChanged();
            }
        }
    }
    public string? Name
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
    public PokemonType Type1
    {
        get => _type1;
        set
        {
            if (_type1 != value)
            {
                _type1 = value;
                OnPropertyChanged();
            }
        }
    }
    public PokemonType Type2
    {
        get => _type2;
        set
        {
            if (_type2 != value)
            {
                _type2 = value;
                OnPropertyChanged();
            }
        }
    }
    public int BaseHP
    {
        get => _baseHP;
        set
        {
            if (_baseHP != value)
            {
                _baseHP = value;
                OnPropertyChanged();
            }
        }
    }

    public PokemonAbility? Ability1
    {
        get => _ability1 != null ? _ability1 : new PokemonAbility();
        set
        {
            if (_ability1 != value)
            {
                _ability1 = value;
                OnPropertyChanged();
            }
        }
    }
    public PokemonAbility? Ability2
    {
        get => _ability2 != null ? _ability2: new PokemonAbility();
        set
        {
            if (_ability2 != value)
            {
                _ability2 = value;
                OnPropertyChanged();
            }
        }
    }
    public PokemonAbility? HiddenAbility
    {
        get => _hiddenAbility != null ? _hiddenAbility : new PokemonAbility{IsHidden = true };
        set
        {
            if (_hiddenAbility != value)
            {
                _hiddenAbility = value;
                OnPropertyChanged();
            }
        }
    }
    public string? EventAbilities
    {
        get => _eventAbilities;
        set
        {
            if (_eventAbilities != value)
            {
                _eventAbilities = value;
                OnPropertyChanged();
            }
        }
    }
    public TrainerRank? RecommendedRank
    {
        get => _recommendedRank;
        set
        {
            if (_recommendedRank != value)
            {
                _recommendedRank = value;
                OnPropertyChanged();
            }
        }
    }
    public GenderType? GenderType
    {
        get => _genderType;
        set
        {
            if (_genderType != value)
            {
                _genderType = value;
                OnPropertyChanged();
            }
        }
    }
    public bool? Legendary
    {
        get => _legendary;
        set
        {
            if (_legendary != value)
            {
                _legendary = value;
                OnPropertyChanged();
            }
        }
    }
    public bool? GoodStarter
    {
        get => _goodStarter;
        set
        {
            if (_goodStarter != value)
            {
                _goodStarter = value;
                OnPropertyChanged();
            }
        }
    }
    public string? _id
    {
        get => __id;
        set
        {
            if (__id != value)
            {
                __id = value;
                OnPropertyChanged();
            }
        }
    }
    public string? DexCategory
    {
        get => _dexCategory;
        set
        {
            if (_dexCategory != value)
            {
                _dexCategory = value;
                OnPropertyChanged();
            }
        }
    }
    public string? DexDescription
    {
        get => _dexDescription;
        set
        {
            if (_dexDescription != value)
            {
                _dexDescription = value;
                OnPropertyChanged();
            }
        }
    }
    public string? BoxSprite
    {
        get => _boxSprite;
        set
        {
            if (_boxSprite != value)
            {
                _boxSprite = value;
                OnPropertyChanged();
            }
        }
    }
    public string? BoxSpriteShiny
    {
        get => _boxSpriteShiny;
        set
        {
            if (_boxSpriteShiny != value)
            {
                _boxSpriteShiny = value;
                OnPropertyChanged();
            }
        }
    }
    public string? BoxSpriteFemale
    {
        get => _boxSpriteFemale;
        set
        {
            if (_boxSpriteFemale != value)
            {
                _boxSpriteFemale = value;
                OnPropertyChanged();
            }
        }
    }
    public string? BoxSpriteFemaleShiny
    {
        get => _boxSpriteFemaleShiny;
        set
        {
            if (_boxSpriteFemaleShiny != value)
            {
                _boxSpriteFemaleShiny = value;
                OnPropertyChanged();
            }
        }
    }
    public Height? Height
    {
        get => _height != null ? _height : new Height();
        set
        {
            if (_height != value)
            {
                _height = value;
                OnPropertyChanged();
            }
        }
    }
    public Weight? Weight
    {
        get => _weight != null ? _weight : new Weight();
        set
        {
            if (_weight != value)
            {
                _weight = value;
                OnPropertyChanged();
            }
        }
    }
    public string? EvolvesFrom
    {
        get => _evolvesFrom;
        set
        {
            if (_evolvesFrom != value)
            {
                _evolvesFrom = value;
                OnPropertyChanged();
            }
        }
    }
    public ObservableCollection<Evolution>? Evolutions
    {
        get => _evolutions != null? _evolutions : new ObservableCollection<Evolution>();
        set
        {
            if (_evolutions != value)
            {
                _evolutions = value;
                OnPropertyChanged();
            }
        }
    }
    public ObservableCollection<PokemonMove>? Moves
    {
        get => _moves ??= new ObservableCollection<PokemonMove>();
        set
        {
            if (_moves != value)
            {
                _moves = value;
                OnPropertyChanged();
            }
        }
    }
    public int MinStrength
    {
        get => _minStrength;
        set
        {
            if (_minStrength != value)
            {
                _minStrength = value;
                OnPropertyChanged();
            }
        }
    }
    public int MaxStrength
    {
        get => _maxStrength;
        set
        {
            if (_maxStrength != value)
            {
                _maxStrength = value;
                OnPropertyChanged();
            }
        }
    }
    public int MinDexterity
    {
        get => _minDexterity;
        set
        {
            if (_minDexterity != value)
            {
                _minDexterity = value;
                OnPropertyChanged();
            }
        }
    }
    public int MaxDexterity
    {
        get => _maxDexterity;
        set
        {
            if (_maxDexterity != value)
            {
                _maxDexterity = value;
                OnPropertyChanged();
            }
        }
    }
    public int MinVitality
    {
        get => _minVitality;
        set
        {
            if (_minVitality != value)
            {
                _minVitality = value;
                OnPropertyChanged();
            }
        }
    }
    public int MaxVitality
    {
        get => _maxVitality;
        set
        {
            if (_maxVitality != value)
            {
                _maxVitality = value;
                OnPropertyChanged();
            }
        }
    }
    public int MinSpecial
    {
        get => _minSpecial;
        set
        {
            if (_minSpecial != value)
            {
                _minSpecial = value;
                OnPropertyChanged();
            }
        }
    }
    public int MaxSpecial
    {
        get => _maxSpecial;
        set
        {
            if (_maxSpecial != value)
            {
                _maxSpecial = value;
                OnPropertyChanged();
            }
        }
    }
    public int MinInsight
    {
        get => _minInsight;
        set
        {
            if (_minInsight != value)
            {
                _minInsight = value;
                OnPropertyChanged();
            }
        }
    }
    public int MaxInsight
    {
        get => _maxInsight;
        set
        {
            if (_maxInsight != value)
            {
                _maxInsight = value;
                OnPropertyChanged();
            }
        }
    }

    public Pokemon()
    {
        _minStrength = 0;
        _maxStrength = 0;
        _minDexterity = 0;
        _maxDexterity = 0;
        _minVitality = 0;
        _maxVitality = 0;
        _minSpecial = 0;
        _maxSpecial = 0;
        _minInsight = 0;
        _maxInsight = 0;
        _moves = new ObservableCollection<PokemonMove>();
        _number = 0;
        _dexID = string.Empty;
        _name = string.Empty;
        _type1 = PokemonType.Normal;
        _type2 = PokemonType.None;
        _baseHP = 0;
        _ability1 = new PokemonAbility();
        _ability2 = new PokemonAbility();
        _hiddenAbility = new PokemonAbility { IsHidden = true };
        _eventAbilities = string.Empty;
        _recommendedRank = TrainerRank.Starter;
        _genderType = Models.GenderType.Regular;
        _legendary = false;
        _goodStarter = false;
        __id = string.Empty;
        _dexCategory = string.Empty;
        _dexDescription = string.Empty;
        _boxSprite = string.Empty;
        _boxSpriteShiny = string.Empty;
        _boxSpriteFemale = string.Empty;
        _boxSpriteFemaleShiny = string.Empty;
        _height = new Height();
        _weight = new Weight();
        _evolvesFrom = string.Empty;
        _evolutions = new ObservableCollection<Evolution>();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public Object Clone()
    {
        return new Pokemon
        {
            Number = this.Number,
            DexID = this.DexID,
            Name = this.Name,
            Type1 = this.Type1,
            Type2 = this.Type2,
            BaseHP = this.BaseHP,
            MinStrength = this.MinStrength,
            MaxStrength = this.MaxStrength,
            MinDexterity = this.MinDexterity,
            MaxDexterity = this.MaxDexterity,
            MinVitality = this.MinVitality,
            MaxVitality = this.MaxVitality,
            MinSpecial = this.MinSpecial,
            MaxSpecial = this.MaxSpecial,
            MinInsight = this.MinInsight,
            MaxInsight = this.MaxInsight,
            Ability1 = this.Ability1 != null ? new PokemonAbility { IsHidden = this.Ability1.IsHidden, Name = this.Ability1.Name } : null,
            Ability2 = this.Ability2 != null ? new PokemonAbility { IsHidden = this.Ability2.IsHidden, Name = this.Ability2.Name } : null,
            HiddenAbility = this.HiddenAbility != null ? new PokemonAbility { IsHidden = this.HiddenAbility.IsHidden, Name = this.HiddenAbility.Name } : null,
            EventAbilities = this.EventAbilities,
            RecommendedRank = this.RecommendedRank,
            GenderType = this.GenderType,
            Legendary = this.Legendary,
            GoodStarter = this.GoodStarter,
            _id = this._id,
            DexCategory = this.DexCategory,
            DexDescription = this.DexDescription,
            BoxSprite = this.BoxSprite,
            BoxSpriteShiny = this.BoxSpriteShiny,
            BoxSpriteFemale = this.BoxSpriteFemale,
            BoxSpriteFemaleShiny = this.BoxSpriteFemaleShiny,
            Height = this.Height != null ? new Height { Meters = this.Height.Meters, Feet = this.Height.Feet } : null,
            Weight = this.Weight != null ? new Weight { Kilograms = this.Weight.Kilograms, Pounds = this.Weight.Pounds } : null,
            EvolvesFrom = this.EvolvesFrom,
            Evolutions = this.Evolutions != null ? new ObservableCollection<Evolution>(this.Evolutions.Select(e => new Evolution { To = e.To, Kind = e.Kind, Item = e.Item, Speed = e.Speed })) : null,
            Moves = this.Moves != null ? new ObservableCollection<PokemonMove>(this.Moves.Select(m => new PokemonMove { LearnedRank = m.LearnedRank, Name = m.Name })) : []
        };
    }
}
