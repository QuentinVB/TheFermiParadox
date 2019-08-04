using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.Core
{

    public class Planet : APhysicalObject
    {
        public Planet()
           : base("x", null)
        {
        }
        public Planet(string name, StellarSystem stellarSystem)
           : base(name, stellarSystem)
        {
        }

        public override string Denomination
        {
            get
            {
                return "A planet";
            }
        }   
        public double LifeSpan { get; set; }

        public double Age { get; set; }

        //TODO
    }
}
