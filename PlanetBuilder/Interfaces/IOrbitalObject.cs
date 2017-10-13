using System;
using PlanetBuilder.Helpers;

namespace PlanetBuilder.Interfaces
{
    public interface IOrbitalObject
    {
        //from field
        double MajorAxis { get; set; } // (in meters)
        double Eccentricity { get; set; } // (no units)
        Angle Inclination { get; set; } // (i in degree)
        Angle LongitudeOfAscendingNode { get; set; }//(in degree)
        Angle ArgumentOfPeriapsis { get; set; }//(in degree)
        Angle TrueAnomaly { get; set; }//(in rad)

        //from math
        TimeSpan OrbitalPeriod { get; set; }//in seconds
        double Apoapsis { get; set; } // (in meters)
        double Periapsis { get; set; } // (in meters)

        Vector3 Position { get; }
        World World { get; }
    }
}