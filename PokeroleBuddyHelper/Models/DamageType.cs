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
    public enum DamageType
    {
        [EnumMember(Value = "")]
        None,
        Physical,
        Special,
        Status
    }
}
