using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.Core
{
    public class Star: APhysicalObject
    {
        public Star()
           : base("x", null)
        {
        }
        public Star(string name, StellarSystem stellarSystem)
           : base(name, stellarSystem)
        {
        }

        public string StarCode { get { return $"{StarClass}{SpectralClass}{SizeCode}"; } }

        public override string Denomination
        {
            get
            {
                string size = (SizeCode == 3) ? "Giant" : "";
                string name = "Unknown";
                if (StarClass == "A") name = "Blue Star";
                if (StarClass == "F") name = "White Star";
                if (StarClass == "G") name = "Yellow Star";
                if (StarClass == "K") name = "Orange Star";
                if (StarClass == "M") name = "Red Dwarf";
                if (StarClass == "D") name = "White Dwarf";
                if (StarClass == "L") name = "Brown Dwarf";
                return size + " " + name;
            }
        }

        [XmlElement("StarType")]
        public string StarClass { get; set; }

        [XmlElement("SizeCode")]
        public int SizeCode { get; set; }

        [XmlElement("SpectralClass")]
        public int SpectralClass { get; set; }

        [XmlElement("Luminosity")]
        public float Luminosity { get; set; }

        [XmlElement("LifeSpan")]
        public float LifeSpan { get; set; }

        [XmlElement("Age")]
        public float Age { get; set; }

        public BasicColor StarColor { get { return PhysicHelpers.ColorTemperatureToARGB((int)SurfaceTemperature); } }

    }
}
