using System.Drawing;
using theFermiParadox.Core.Abstracts;
using theFermiParadox.Core.Interfaces;
using theFermiParadox.Core.Utilities;

namespace theFermiParadox.Core
{
    public class BlackHole : APhysicalObject, IStellar
    {
        //THE INFAMOUS SPOOOKY
        public BlackHole()
        {

        }

        public override string Denomination
        {
            get
            {
                return "Black Hole";
            }
        }
        public double SchwarzschildRadius { get { return 2 * Mass * Physic.GravitationalConstant / Physic.LightSpeed * Physic.LightSpeed; } }
        public int ElectricCharge { get { return 0; } }

        public Color DisplayColor => Color.Black;

        public override double Volume => Physic.SphereVolume(SchwarzschildRadius);
    }
}