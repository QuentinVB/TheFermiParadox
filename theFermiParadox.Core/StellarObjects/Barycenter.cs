using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.Core
{
    public class Barycenter : APhysicalObject
    {
        public Barycenter() 
        {
        }

        public override string Denomination
        {
            get
            {
                return "Barycenter";
            }
        }
        //virtual mass ?
    }
}