using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleBuddyHelper.Models
{
    public class Evolution
    {
        public string? To { get; set; } = string.Empty;
        public string? Kind { get; set; } = string.Empty;
        public string? Item { get; set; } = string.Empty;
        public int Speed { get; set; }
    }
}

