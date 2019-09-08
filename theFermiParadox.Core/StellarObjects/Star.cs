using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.Core
{
    //unused
    public enum StarClass
    {
        A,//Blue
        F,//White
        G,//Yellow
        K,//Orange
        M,//Red Dwarf
        D,//White Dwarf
        L//Brown Dwarf
    }
    public class Star : APhysicalObject, IStellar
    {
        public Star()
           : base(null)
        {
        }
        public Star(StellarSystem stellarSystem)
           : base(stellarSystem)
        {
        }

        public string StarCode { get { return $"{StarClass}{SpectralClass}{SizeCodeLatin}"; } }

        public override string Denomination
        {
            get
            {
                string name;
                switch (StarClass)
                {
                    case "A": name = "Blue Star"; break;
                    case "F": name = "White Star"; break;
                    case "G": name = "Yellow Star"; break;
                    case "K": name = "Orange Star"; break;
                    case "M": name = "Red Dwarf"; break;
                    case "D": name = "White Dwarf"; break;
                    case "L": name = "Brown Dwarf"; break;
                    default:name = "Unknown"; break;
                }
                return (SizeCode == 3) ? "Giant " : "" + name;
            }
        }

        public string StarClass { get; set; }

        public int SpectralClass { get; set; }

        public int SizeCode { get; set; }

        public string SizeCodeLatin
        {
            get
            {
                return Physic.LatinNumber(SizeCode);
            }
        }


        public double Luminosity { get; set; }

        public double LifeSpan { get; set; }

        public double Age { get; set; }


        public BasicColor DisplayColor { get { return Physic.ColorTemperatureToRGB((int)SurfaceTemperature); } }

    }
}
