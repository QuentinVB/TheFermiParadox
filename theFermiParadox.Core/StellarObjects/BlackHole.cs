using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.Core
{
    public class BlackHole : APhysicalObject
    {
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
        public double SchwarzschildRadius { get { return 2 * Mass * PhysicHelpers.GravitationalConstant / PhysicHelpers.LightSpeed * PhysicHelpers.LightSpeed; } }
        public int ElectricCharge { get { return 0; } }
    }
}