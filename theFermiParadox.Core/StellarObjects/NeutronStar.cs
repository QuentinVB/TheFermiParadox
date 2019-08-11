using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.Core
{
    public class NeutronStar : APhysicalObject, IStellar
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

        BasicColor IStellar.DisplayColor => throw new System.NotImplementedException();

        
    }
}