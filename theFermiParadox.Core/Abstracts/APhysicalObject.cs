using System;
using theFermiParadox.Core.Interfaces;
using theFermiParadox.Core.Utilities;

namespace theFermiParadox.Core.Abstracts
{
    public abstract class APhysicalObject : ABody, IOrbitable
    {
        private double _radius;

        private TimeSpan _rotationPeriod;
        private double _angularVelocity;
        public APhysicalObject()
            : base(null,false)
        { }
        
        public APhysicalObject( StellarSystem stellarSystem)
            : base(stellarSystem, false)
        {

        }
        #region Mass behavior
        //TODO : having a constistent unit system is mandatory to switch from solar mass to earth mass to tons and kg
        public override double Mass { get; set; } // in Solar Mass...
        /// <summary>
        /// The absolute density (above 1 : sink in water, below 1 : float)
        /// </summary>
        public double Density { get => Mass / Volume; }
        /// <summary>
        /// surface gravity based on g' acceleration, in m.s-2
        /// </summary>
        public double SurfaceGravity => (Physic.GravitationalConstant * Mass) / (RadiusInM * RadiusInM); //m.s-2
        /// <summary>
        /// Speed to escape the gravity well, in m.s-1 
        /// </summary>
        public double EscapeVelocity { get => Math.Sqrt((2 * Physic.GravitationalConstant * Mass) / RadiusInM); }
        #endregion
        #region Rotation behavior
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
        /// <summary>
        /// rotation period of the physical object
        /// </summary>
        public TimeSpan RotationPeriod { 
            get=> _rotationPeriod;  // in s
            set {
                _rotationPeriod = value;
                _angularVelocity = 2 * Math.PI / _rotationPeriod.Seconds;
            } 
        }
        #endregion
        #region Age behavior
        public double LifeSpan { get; set; }

        public double Age { get; set; }
        #endregion
        #region Shape behavior
        /// <summary>
        /// average radius of the physical object, in m
        /// </summary>
        public double RadiusInM { get => _radius * Physic.SolarRadius; set => _radius = value / Physic.SolarRadius; }
        //TODO : again with the conversion unit... should be using meters instead
        public double Radius { get; set; } // in solar Radii
        public abstract double Volume { get; }
        #endregion

        /// <summary>
        /// The blackbody surface temperature, different from the true Temperature
        /// </summary>
        public double SurfaceTemperature { get; set; } // Kelvin

        //TODO : should be the dotproduct of the vectors
        public double Speed { get; set; } // m.s-1


        public override void Accept(Visitor v) => v.Visit(this);
        public override INode Accept(MutationVisitor v) => v.Visit(this);
    }
}
