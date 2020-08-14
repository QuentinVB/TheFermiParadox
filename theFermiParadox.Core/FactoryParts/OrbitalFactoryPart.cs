using System;
using System.Collections.Generic;
using System.Text;
using theFermiParadox.Core.Abstracts;
using theFermiParadox.DAL;

namespace theFermiParadox.Core
{
    public partial class SystemFactory
    {
        private double BARYCENTERTHRESHOLD = 6;

        private Orbit ForgeOrbit(IOrbitable A, IOrbitable B, double systemAge)
        {
            return ForgeOrbit( A, B,systemAge, 0);
        }
        /// <summary>
        /// forge the binary orbit and return the root element (barycenter or the more massive)
        /// </summary>
        /// <param name="stellarSystem"></param>
        /// <param name="bodyA"></param>
        /// <param name="bodyB"></param>
        /// <param name="systemAge"></param>
        /// <returns></returns>
        IOrbitable ForgeBinaryOrbit(ref StellarSystem stellarSystem,APhysicalObject bodyA, APhysicalObject bodyB, double systemAge)
        {
            //TODO : add barycenter avoidance for large object ?
            if (bodyA.Mass > bodyB.Mass * BARYCENTERTHRESHOLD)//B mass is significantly 10 times less than A mass, then no barycenter
            {
                
                Orbit orbit = ForgeOrbit(bodyA, bodyB, systemAge);
                stellarSystem.Orbits.Add(orbit);

                return orbit.MainBody;
            }
            else// B mass is close to A mass : barycenter orbit
            {
                Barycenter barycenter = new Barycenter(stellarSystem, bodyA, bodyB)
                {
                    Name = $"{stellarSystem.Name} {Physic.StarLetter(bodyA.BodyIndex - 1)}{Physic.StarLetter(bodyB.BodyIndex - 1)}" 
                };
                stellarSystem.Add(barycenter);

                //TODO : force resonant orbit for barycenter 
                Orbit orbit = ForgeOrbit(barycenter, bodyA, systemAge);
                stellarSystem.Orbits.Add(orbit);

                Orbit orbit2 = ForgeOrbit(barycenter, bodyB, systemAge);
                orbit2.OffsetPeriod(0.5);

                stellarSystem.Orbits.Add(orbit2);

                return barycenter;
            }
        }

        private Orbit ForgeOrbit(IOrbitable A, IOrbitable B, double systemAge, int meanSeparationOffset)
        {
            //double periapsis;
            double meanDistance = 0;
            double orbitalEccentricity = 0;


            int OrbitalEccentricityRand = 0;

            //Mean Separation p10
            int meanAnomalyRand = randomSource.NextInclusive(1, 10)+ meanSeparationOffset;
            if (systemAge > 5) meanAnomalyRand++;
            else if (systemAge < 1) meanAnomalyRand--;
            meanAnomalyRand = Physic.Clamp(meanAnomalyRand, 1, 10);
                
            //edge case ?
            /*
            if(A.Radius * Physic.SolarRadius + B.Radius * Physic.SolarRadius < 0.05f )
            {
            }*/
            if (1 <= meanAnomalyRand && meanAnomalyRand <= 3) { meanDistance = randomSource.NextInclusive(1, 10) * 0.05f; OrbitalEccentricityRand -= 2; }//AU
            else if (4 <= meanAnomalyRand && meanAnomalyRand <= 6) { meanDistance = randomSource.NextInclusive(1, 10) * 0.5f; OrbitalEccentricityRand--; } //AU
            else if (7 <= meanAnomalyRand && meanAnomalyRand <= 8) meanDistance = randomSource.NextInclusive(1, 10) * 3; //AU
            else if (meanAnomalyRand == 9) meanDistance = randomSource.NextInclusive(1, 10) * 20; //AU
            else if (meanAnomalyRand == 10) meanDistance = randomSource.NextInclusive(1, 100) * 200; //AU

            //Orbital Eccentricity
            OrbitalEccentricityRand += randomSource.NextInclusive(1, 10);
            OrbitalEccentricityRand = Physic.Clamp(OrbitalEccentricityRand, 1, 10);
            int adder = randomSource.NextInclusive(1, 10);

            if (1 <= OrbitalEccentricityRand && OrbitalEccentricityRand <= 2) orbitalEccentricity = adder * 0.01f;
            else if (3 <= OrbitalEccentricityRand && OrbitalEccentricityRand <= 4) orbitalEccentricity = 0.01f + adder * 0.01f;
            else if (5 <= OrbitalEccentricityRand && OrbitalEccentricityRand <= 6) orbitalEccentricity = 0.02f + adder * 0.01f;
            else if (7 <= OrbitalEccentricityRand && OrbitalEccentricityRand <= 8) orbitalEccentricity = 0.03f + adder * 0.01f;
            else if (OrbitalEccentricityRand == 9) orbitalEccentricity = 0.04f + adder * 0.01f;
            else if (OrbitalEccentricityRand == 10) orbitalEccentricity = 0.05f + adder * 0.04f;

            //periapsis = meanDistance * Physic.AstronomicUnit * (1 + orbitalEccentricity);



            Orbit orbit;
            //Finaly, put together
            if (_testMode)
            {
                //orbit = new Orbit(A, B, DateTime.Now, 0.0,1 * Physic.AstronomicUnit);
                orbit = new Orbit(A, B, DateTime.Now, 0.8,3 * Physic.AstronomicUnit);
            }
            else
            {
                orbit = new Orbit(A, B, DateTime.Now, orbitalEccentricity, meanDistance * Physic.AstronomicUnit);
                
                
                orbit.Inclination = Math.PI * randomSource.NextDouble()* 0.75 * orbitalEccentricity; //in radian

            }


            return orbit;
        }
    }
}
