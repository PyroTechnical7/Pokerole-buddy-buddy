using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PokeroleBuddyHelper.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TargetType
    {
        [EnumMember(Value = "One Ally")]
        Ally,
        Foe,
        User,
        Area,
        [EnumMember(Value = "User and Allies")]
        UserAndAllies,
        [EnumMember(Value = "All Foes")]
        AllFoes,


    }
}
