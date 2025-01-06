using System.Drawing;
using theFermiParadox.Core.Abstracts;
using theFermiParadox.Core.Interfaces;
using theFermiParadox.Core.Utilities;

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
        public override double Volume => Physic.SphereVolume(RadiusInM);
        public Color DisplayColor { get { return Physic.ColorTemperatureToRGB((int)SurfaceTemperature); } }
        public double Luminosity { get; set; }
    }
}
