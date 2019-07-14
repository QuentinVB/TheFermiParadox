using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace theFermiParadox.Core
{
    public struct DwarfStar
    {
        public int Temperature { get; set; }
        public double Radius { get; set; }
        public double Mass { get; set; }
        public int Random { get; set; }
    }

}

