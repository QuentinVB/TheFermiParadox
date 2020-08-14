using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using theFermiParadox.Core.Abstracts;
using theFermiParadox.Core.Utilities;

namespace theFermiParadox.Core
{
    //https://en.wikipedia.org/wiki/Kepler%27s_laws_of_planetary_motion#Position_as_a_function_of_time
    //https://space.stackexchange.com/questions/8911/determining-orbital-position-at-a-future-point-in-time
    public class Orbit : APrintable,INode, ICloneable
    {
        private double _eccentricity;
        private Angle _meanAnomaly;

        private double _apoapsis;
        private double _periapsis;

        private Angle _inclination = 0;
        private Angle _longitudeOfAscendingNode = 0;
        private Angle _argumentOfPeriapsis = 0;

        private readonly DateTime _startDate;
        private ulong _orbitalPeriod;
        private ulong _periodOffset=0;

        private ulong _time;
        private double _eccentricAnomaly = 0;


        IOrbitable _mainBody;
        IOrbitable _body;



        /// <summary>
        /// Define the orbit between 2 body. The most massive will be the main body.
        /// </summary>
        /// <param name="bodyA">The first body of the orbit</param>
        /// <param name="bodyB">The second body of the orbit</param>
        /// <param name="epoch">The t0 date of the system</param>
        /// <param name="eccentricity">derivation from perfect circle : 0 is circle, below 1 is elipsoid, above is a escape trajectory (no units)</param>
        /// <param name="meanDistance">aka the semi Major axis of the orbit (in meters)</param>
        //TODO : should exist a constructor with other parameter (major axis, minor axis for instance)
        public Orbit(IOrbitable bodyA, IOrbitable bodyB, DateTime epoch, double eccentricity, double meanDistance)
            :this(bodyA,bodyB)
        {    
            //initialise internal value
            _startDate = epoch;
            _time = 0;
            _eccentricity = eccentricity;
            _meanAnomaly = 0;
            _apoapsis = meanDistance * (1 + eccentricity);
            _periapsis = meanDistance * (1 - eccentricity);

            //THAR BE DRAGONZ
            _orbitalPeriod = (ulong)(2 * Math.PI * Math.Sqrt(SemiMajorAxis * SemiMajorAxis * SemiMajorAxis / (Physic.GravitationalConstant * _mainBody.Mass * Physic.SolarMass)));

            Body.Position = CurrentBodyPosition;
        }
        private Orbit(IOrbitable bodyA, IOrbitable bodyB)
        {
            //Setup bodies position according to their mass, maybe a better approach ?

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

            //Bind the body to this orbit
            _mainBody.ChildOrbits.Add(this);
            _body.ParentOrbit = this;

        }
        //INITIALS DATA
        /// <summary>
        /// farthest point to its orbital focus (in meters)
        /// </summary>
        public double Apoapsis { get => _apoapsis; }
        /// <summary>
        /// nearest point to its orbital focus (in meters)
        /// </summary>
        public double Periapsis { get => _periapsis; }
        /// <summary>
        /// period around the orbital focus (in seconds)
        /// </summary>
        public ulong OrbitalPeriod { get => _orbitalPeriod; }
        /// <summary>
        /// derivation from perfect circle : 0 is circle, below 1 is elipsoid, above is a escape trajectory (no units)
        /// </summary>
        public double Eccentricity { get => _eccentricity; }
        /// <summary>
        /// time since t0 (in seconds)
        /// </summary>
        public ulong ElapsedTime { get => _time; }
        /// <summary>
        /// The t0 of the orbit, when smaller body is at periapsis, used to calculate every position from this reference point in time
        /// </summary>
        public DateTime Epoch => _startDate;
        /// <summary>
        /// The actual time of the orbit, in human from earth readable format
        /// </summary>
        public DateTime CurrentDate => _startDate.AddSeconds(_time);

        //DERIVED
        /// <summary>
        /// the major axis, the longest diameter (in meters)
        /// </summary>
        public double MajorAxis { get => _periapsis + _apoapsis; }
        /// <summary>
        /// the semi major axis, the longest diameter divided by 2(in meters)
        /// </summary>
        public double SemiMajorAxis { get => MajorAxis / 2; }
        /// <summary>
        /// the minor axis, the shortest diameter (in meters)
        /// </summary>
        public double MinorAxis { get => SemiMinorAxis * 2; }
        /// <summary>
        /// the minor axis, the shortest diameter (in meters)
        /// </summary>
        public double SemiMinorAxis { get => Math.Sqrt(_apoapsis * _periapsis); }
        /// <summary>
        /// the chord above the main focus (in meters)
        /// </summary>
        public double SemiLatusRectum { get => Math.Pow(SemiMajorAxis, 2) / SemiMajorAxis; }
        /// <summary>
        /// the distance between the center to a focus (in meters)
        /// </summary>
        public double LinearEccentricity { get => Math.Sqrt(SemiMajorAxis*SemiMajorAxis- SemiMinorAxis * SemiMinorAxis); }
        /// <summary>
        /// The mean motion around the orbit (in rad.s-1)
        /// </summary>
        public double MeanMotion { get => 2 * Math.PI / OrbitalPeriod; }

        //VARIABLE IN TIME
        /// <summary>
        /// Updated each time the time update (in rad.s-1)
        /// </summary>
        public Angle MeanAnomaly { get => _meanAnomaly; }
        /// <summary>
        /// The current time of the system, thus derivate all the motion calculus (in s)
        /// </summary>
        public ulong CurrentTime { get => _time; }
        /// <summary>
        /// The local eccentric anomaly, recalculated everytime the time update (no unit)
        /// </summary> 
        public double EccentricAnomaly { get => _eccentricAnomaly; }
        /// <summary>
        /// the current distance between the main body and the smaller body (in m)
        /// </summary>
        public double CurrentRadius { get => SemiMajorAxis * (1 - Math.Cos(EccentricAnomaly)); }
        /// <summary>
        /// the real angle between periapsis and actual position of the smaller body on the orbit (in rad)
        /// </summary>
        public Angle TrueAnomaly { get => 2 * Math.Atan(Math.Sqrt((1 + Eccentricity) / (1 - Eccentricity)) * Math.Tan(EccentricAnomaly / 2)); }

        //INCLINAISON (NOT SUPPORTED YET)
        /// <summary>
        /// inclination of the orbit from ecliptic (i in radian)
        /// </summary>
        public Angle Inclination { get => _inclination; set => _inclination = value; }
        /// <summary>
        ///(in degree)
        /// </summary>
        //public Angle LongitudeOfAscendingNode { get => _longitudeOfAscendingNode; }
        /// <summary>
        /// (in degree)
        /// </summary>
        //public Angle ArgumentOfPeriapsis { get => _argumentOfPeriapsis; }


        //DRAWING FUNCTIONS
        public Vector3 MainBodyOffset { get => new Vector3(MajorAxis - Periapsis, 0, 0); }

        [NonPrintable]
        public IOrbitable MainBody { get => _mainBody; }
        [NonPrintable]
        public IOrbitable Body { get => _body; }

        public double Width { get => MajorAxis; }
        public double Height { get => MinorAxis; }
        /// <summary>
        /// The current body position, in the cartesian vector of the center of the orbit
        /// </summary>
        public Vector3 CurrentBodyPosition => new Vector3(
            SemiMajorAxis * (Math.Cos(EccentricAnomaly) - Eccentricity),
            SemiMinorAxis * Math.Sin(EccentricAnomaly),
            0
            );


        public void UpdateTime(TimeSpan timeOffset)
        {
            //THAR BE DRAGONZ

            _time += (uint)timeOffset.TotalSeconds;
            //add mean anomaly at 0 ?
            _meanAnomaly = (MeanMotion * (_time+_periodOffset)) % (2 * Math.PI);
            _eccentricAnomaly = ComputeEccentricAnomaly(Eccentricity, MeanAnomaly);

            //update
            Body.Position = CurrentBodyPosition;
        }

        private double ComputeEccentricAnomaly(double eccentricity, double meanAnomaly)
        {
            double E = meanAnomaly;
            double dE = 1;
            while (Math.Abs(dE) < 1e-6)
            {
                dE = (E - eccentricity * Math.Sin(E) - meanAnomaly) / (1 - eccentricity * Math.Cos(E));
                E -= dE;
            }
            return E;
        }
        

        public object Clone()
        {
            return this.MemberwiseClone(); ;
        }
        /// <summary>
        /// Offset the time of the period to match realistic resonnant orbit for instance
        /// </summary>
        /// <param name="fractionOfPeriod">the fraction of the period to offset</param>
        internal void OffsetPeriod(double fractionOfPeriod)
        {
            if (0 > fractionOfPeriod || fractionOfPeriod > 1) throw new ArgumentOutOfRangeException("the fraction should be between 0 and 1");
            _periodOffset = (ulong)Math.Round(OrbitalPeriod * fractionOfPeriod);
        }
        public void Accept(Visitor v) => v.Visit(this);
        public INode Accept(MutationVisitor v) => v.Visit(this);
    }
}
