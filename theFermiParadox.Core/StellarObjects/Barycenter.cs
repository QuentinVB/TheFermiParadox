using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.Core
{
    public class Barycenter : ABody,IOrbitable
    {
       
        public Barycenter()
            : base("x", null)
        { }

        public Barycenter(string name)
            : base(name, null)
        { }

        public Barycenter(string name, StellarSystem stellarSystem)
            : base(name, stellarSystem)
        {

        }

        public override string Denomination
        {
            get
            {
                return "Barycenter";
            }
        }

        public double Radius { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        //virtual mass ?

        public double Mass { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}