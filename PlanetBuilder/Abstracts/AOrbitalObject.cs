using PlanetBuilder.Helpers;
using PlanetBuilder.Interfaces;
using System;
using System.Xml.Serialization;

namespace PlanetBuilder.Abstracts
{
    public abstract class AOrbitalObject<T> : AXMLSerializable<T>, IOrbitalObject
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

        private readonly World _world;
        private Vector3 _position;

        protected AOrbitalObject(string name, World world) : base(name)
        {
            this._world = world;
        }

        /// <summary>
        /// the major axis, the longest diameter (in meters)
        /// </summary>
        public double MajorAxis { get => _majorAxis; set => _majorAxis = value; }
        /// <summary>
        /// derivation from perfect circle : 0 is circle, below 1 is elipsoid, above is a escape trajectory (no units)
        /// </summary>
        public double Eccentricity { get => _eccentricity; set => _eccentricity = value; }
        /// <summary>
        /// inclination of the orbit from ecliptic (i in degree)
        /// </summary>
        public Angle Inclination { get => _inclination; set => _inclination = value; }
        /// <summary>
        ///(in degree)
        /// </summary>
        public Angle LongitudeOfAscendingNode { get => _longitudeOfAscendingNode; set => _longitudeOfAscendingNode = value; }
        /// <summary>
        /// (in degree)
        /// </summary>
        public Angle ArgumentOfPeriapsis { get => _argumentOfPeriapsis; set => _argumentOfPeriapsis = value; }
        /// <summary>
        /// angle between periapsis and actual position on the orbit (i in rad)
        /// </summary>
        public Angle TrueAnomaly { get => _trueAnomaly; set => _trueAnomaly = value; }

        /*FROM MATH*/

        /// <summary>
        /// farthest point to its orbital focus (in meters)
        /// </summary>
        public double Apoapsis { get => _apoapsis; set => _apoapsis = value; }
        /// <summary>
        /// nearest point to its orbital focus (in meters)
        /// </summary>
        public double Periapsis { get => _periapsis; set => _periapsis = value; }
        /// <summary>
        /// period aroud the orbital focus (in seconds)
        /// </summary>
        public TimeSpan OrbitalPeriod { get => _orbitalPeriod; set => _orbitalPeriod = value; }
       


        public Vector3 Position { get { return new Vector3(); } }
        

        [XmlIgnore]
        public World World { get => _world; }


        /* properties
         * Orbital speed
         * mean anomaly in degree
         * _eccentricity; // 
         * LOTS OF MATHS HERE
         */
    }
}
