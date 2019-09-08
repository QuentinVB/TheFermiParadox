using theFermiParadox.Core.Abstracts;

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

        public BasicColor DisplayColor => BasicColor.Black;
    }
}