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
            : base("x", null)
        { }

        public APhysicalObject(string name)
            : base(name, null)
        {}

        public APhysicalObject(string name, StellarSystem stellarSystem)
            : base(name, stellarSystem)
        {

        }

        public double Mass { get; set; } // in Solar Mass

        public double AngularMomentum { get; set; } // rad.s-1

        public double Speed { get; set; } // m.s-1

        public double SurfaceTemperature { get; set; } // Kelvin

        public double Radius { get; set; } // in solar Radii

    }
}
