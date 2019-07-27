using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.Core
{
    public class Orbit
    {
        private double _majorAxis;
        private double _eccentricity;
        private Angle _inclination;
        private Angle _longitudeOfAscendingNode;
        private Angle _argumentOfPeriapsis;
        private Angle _trueAnomaly;

        private double _apoapsis;
        private double _periapsis;
        private TimeSpan _orbitalPeriod;

        private Vector3 _position;

        ABody _mainBody;
        ABody _body;

        /// <summary>
        /// Define the orbital parameter between 2 body. The most massive will be the main body.
        /// </summary>
        /// <param name="bodyA"></param>
        /// <param name="bodyB"></param>
        public Orbit(ABody bodyA, ABody bodyB) 
        {
            if (bodyA.IsVirtual) { _mainBody = bodyA; _body = bodyB; }
            else if (bodyB.IsVirtual) { _mainBody = bodyB; _body = bodyA; }
            else if (bodyA is APhysicalObject bodyAphysical && bodyB is APhysicalObject bodyBphysical)
            {
                if (bodyAphysical.Mass >= bodyBphysical.Mass)
                {
                    _mainBody = bodyA; _body = bodyB;
                }
                else
                {
                    _mainBody = bodyB; _body = bodyA;
                }
            }
            else
            {
                _mainBody = bodyA; _body = bodyB;
            }

            _mainBody.ChildOrbit.Add(this);
            _body.ParentOrbit = this;
        }

        /// <summary>
        /// the major axis, the longest diameter (in meters)
        /// </summary>
        public double MajorAxis { get => _majorAxis; }
        /// <summary>
        /// derivation from perfect circle : 0 is circle, below 1 is elipsoid, above is a escape trajectory (no units)
        /// </summary>
        public double Eccentricity { get => _eccentricity; internal set => _eccentricity = value; }
        /// <summary>
        /// inclination of the orbit from ecliptic (i in degree)
        /// </summary>
        public Angle Inclination { get => _inclination; }
        /// <summary>
        ///(in degree)
        /// </summary>
        public Angle LongitudeOfAscendingNode { get => _longitudeOfAscendingNode; }
        /// <summary>
        /// (in degree)
        /// </summary>
        public Angle ArgumentOfPeriapsis { get => _argumentOfPeriapsis; }
        /// <summary>
        /// angle between periapsis and actual position on the orbit (i in rad)
        /// </summary>
        public Angle TrueAnomaly { get => _trueAnomaly;  }



        /// <summary>
        /// farthest point to its orbital focus (in meters)
        /// </summary>
        public double Apoapsis { get => _apoapsis; internal set => _apoapsis=value; }
        /// <summary>
        /// nearest point to its orbital focus (in meters)
        /// </summary>
        public double Periapsis { get => _periapsis; internal set => _periapsis = value; }
        /// <summary>
        /// period aroud the orbital focus (in seconds)
        /// </summary>
        public TimeSpan OrbitalPeriod { get => _orbitalPeriod; }



        public Vector3 Position { get { return new Vector3(); } }

        public ABody MainBody { get => _mainBody; }
        public ABody Body { get => _body; }




        /* properties
         * Orbital speed
         * mean anomaly in degree
         * _eccentricity; // 
         * LOTS OF MATHS HERE
         */
    }
}
