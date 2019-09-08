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

        public override double Mass { get => _bodyA.Mass + _bodyB.Mass; set => throw new InvalidOperationException(); }

        public BasicColor DisplayColor => BasicColor.Black;

        public override void Accept(Visitor v) => v.Visit(this);
        public override Node Accept(MutationVisitor v) => v.Visit(this);


    }
}