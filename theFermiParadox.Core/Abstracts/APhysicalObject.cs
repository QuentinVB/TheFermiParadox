using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using theFermiParadox.Core;

namespace theFermiParadox.Core.Abstracts
{
    public abstract class APhysicalObject : ABody
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

        [XmlElement("Mass")]
        public float Mass { get; set; } // kg

        [XmlElement("AngularMomentum")]
        public float AngularMomentum { get; set; } // rad.s-1

        [XmlElement("Speed")]
        public float Speed { get; set; } // m.s-1

        [XmlElement("SurfaceTemperature")]
        public float SurfaceTemperature { get; set; } // Kelvin
    }
}
