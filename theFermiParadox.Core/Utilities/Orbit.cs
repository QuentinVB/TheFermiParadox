using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.Core
{
    public class Orbit:IOrbit
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

        protected Orbit(ABody mainBody, ABody body) 
        {
            _mainBody = mainBody;
            _body = body;
        }

        /// <summary>
        /// the major axis, the longest diameter (in meters)
        /// </summary>
        public double MajorAxis { get => _majorAxis; }
        /// <summary>
        /// derivation from perfect circle : 0 is circle, below 1 is elipsoid, above is a escape trajectory (no units)
        /// </summary>
        public double Eccentricity { get => _eccentricity; }
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

        /*FROM MATH*/

        /// <summary>
        /// farthest point to its orbital focus (in meters)
        /// </summary>
        public double Apoapsis { get => _apoapsis;  }
        /// <summary>
        /// nearest point to its orbital focus (in meters)
        /// </summary>
        public double Periapsis { get => _periapsis;  }
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
