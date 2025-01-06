using System.Drawing;
using theFermiParadox.Core.Abstracts;
using theFermiParadox.Core.Interfaces;
using theFermiParadox.Core.Utilities;

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

        public override double Volume => Physic.SphereVolume(RadiusInM);

        Color IStellar.DisplayColor => throw new System.NotImplementedException();
    }
}