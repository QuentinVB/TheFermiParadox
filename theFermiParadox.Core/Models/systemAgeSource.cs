using System.Collections.Generic;
using System.Xml.Serialization;

namespace theFermiParadox.Core
{
    public struct StarAge
    {
        public string StarClass { get; set; }
        public int SpectralClassMin { get; set; }
        public int SpectralClassMax { get; set; }
        public int Random { get; set; }
        public double LifeSpan { get; set; }
        public double Age { get; set; }
        public double LuminosityModifier { get; set; }
        public double TemperatureRollModifier { get; set; }
    }
}
