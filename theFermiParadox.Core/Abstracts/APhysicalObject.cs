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
        private TimeSpan _rotationPeriod;
        private double _angularVelocity;
        public APhysicalObject()
            : base(null,false)
        { }
        
        public APhysicalObject( StellarSystem stellarSystem)
            : base(stellarSystem, false)
        {

        }
        //todo : having a constistent unit system is mandatory to switch from solar mass to earth mass to tons and kg
        public override double Mass { get; set; } // in Solar Mass... 

        //https://fr.wikipedia.org/wiki/Moment_cinétique
        public double AngularMomentum { get; set; } // 	kg⋅m2⋅s−1

        //get/set update pattern...
        public double AngularVelocity { 
            get=> _angularVelocity; // rad.s-1
            set {
                _angularVelocity = value;
                _rotationPeriod = TimeSpan.FromSeconds(2 * Math.PI / _angularVelocity);
            }
        } 
        public TimeSpan RotationPeriod { 
            get=> _rotationPeriod;  // in s
            set {
                _rotationPeriod = value;
                _angularVelocity = 2 * Math.PI / _rotationPeriod.Seconds;
            } 
        } 


        public double Speed { get; set; } // m.s-1

        public double SurfaceTemperature { get; set; } // Kelvin

        public double Radius { get; set; } // in solar Radii

        public override void Accept(Visitor v) => v.Visit(this);
        public override INode Accept(MutationVisitor v) => v.Visit(this);
    }
}
