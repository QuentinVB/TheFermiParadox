using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using theFermiParadox.Core;

namespace theFermiParadox.Core.Abstracts
{
    public abstract class APhysicalObject : ABody, IOrbitable
    {
        public APhysicalObject()
            : base(null,false)
        { }
        
        public APhysicalObject( StellarSystem stellarSystem)
            : base(stellarSystem, false)
        {

        }

        public override double Mass { get; set; } // in Solar Mass

        public double AngularMomentum { get; set; } // rad.s-1

        public double Speed { get; set; } // m.s-1

        public double SurfaceTemperature { get; set; } // Kelvin

        public double Radius { get; set; } // in solar Radii

        public override void Accept(Visitor v) => v.Visit(this);
        public override INode Accept(MutationVisitor v) => v.Visit(this);
    }
}
