using System;
using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.Core
{
    public class Barycenter : ABody,IOrbitable, IStellar
    {
        APhysicalObject _bodyA;
        APhysicalObject _bodyB;
        
        public Barycenter(StellarSystem stellarSystem, APhysicalObject bodyA, APhysicalObject bodyB)
            : base(stellarSystem, true)
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

        //reduced mass
        //https://en.wikipedia.org/wiki/Reduced_mass
        //https://en.wikipedia.org/wiki/Two-body_problem#Reduction_to_two_independent,_one-body_problems
        public override double Mass { get => (_bodyA.Mass * _bodyB.Mass) / (_bodyA.Mass + _bodyB.Mass); set => throw new InvalidOperationException(); }
        public double TrueMass { get => _bodyA.Mass + _bodyB.Mass; set => throw new InvalidOperationException(); }
        public BasicColor DisplayColor => BasicColor.Black;

        public override void Accept(Visitor v) => v.Visit(this);
        public override INode Accept(MutationVisitor v) => v.Visit(this);
    }
}