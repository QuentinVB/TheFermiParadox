using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.Core
{
    public class NeutronStar : APhysicalObject
    {
        public NeutronStar() 
        {
        }

        public override string Denomination
        {
            get
            {
                return "Neutron Star";
            }
        }
    }
}