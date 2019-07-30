using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.Core
{
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
    public class Star : APhysicalObject
    {
        public Star()
           : base("x", null)
        {
        }
        public Star(string name, StellarSystem stellarSystem)
           : base(name, stellarSystem)
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
                switch (SizeCode)
                {
                    case 0: return "-";
                    case 1: return "I";
                    case 2: return "II";
                    case 3: return "III";
                    case 4: return "IV";
                    case 5: return "V";
                    case 6: return "VI";
                    case 7: return "VII";
                    case 8: return "VIII";
                    case 9: return "IX";
                    default: return "";
                }
            }
        }


        public double Luminosity { get; set; }

        public double LifeSpan { get; set; }

        public double Age { get; set; }


        public BasicColor StarColor { get { return Physic.ColorTemperatureToARGB((int)SurfaceTemperature); } }

    }
}
