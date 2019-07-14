using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace theFermiParadox.Core.Models
{
    public struct StarGeneration
    {
        public int RangeMin { get; set; }
        public int RangeMax { get; set; }
        public string Type { get; set; }
        public int SizeCode { get; set; }
    }
}
