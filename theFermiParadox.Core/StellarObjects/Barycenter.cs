using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.Core
{
    public class Barycenter : ABody,IOrbitable
    {
        APhysicalObject _bodyA;
        APhysicalObject _bodyB;
        
        public Barycenter(string name, StellarSystem stellarSystem, APhysicalObject bodyA, APhysicalObject bodyB)
            : base(name, stellarSystem, true)
        {
            _bodyA = bodyA;
            _bodyB = bodyB;
        }
        public override string Denomination
        {
            get
            {
                return "Barycenter";
            }
        }

        public double Radius { get { return 0; } }
        //virtual mass ?

        public double Mass { get => _bodyA.Mass + _bodyB.Mass; }
    }
}