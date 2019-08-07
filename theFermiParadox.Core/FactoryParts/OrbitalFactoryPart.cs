using System;
using System.Collections.Generic;
using System.Text;
using theFermiParadox.Core.Abstracts;
using theFermiParadox.DAL;

namespace theFermiParadox.Core
{
    public partial class SystemFactory
    { 
        
        private Orbit ForgeOrbit(IOrbitable A, IOrbitable B, double systemAge, int meanSeparationOffset)
        {
            double periapsis;
            double meanDistance = 0;
            double orbitalEccentricity = 0;

            do
            {


                int OrbitalEccentricityRand = 0;

                //Mean Separation p10
                int meanAnomalyRand = randomSource.Next(1, 10)+ meanSeparationOffset;
                if (systemAge > 5) meanAnomalyRand++;
                else if (systemAge < 1) meanAnomalyRand--;
                meanAnomalyRand = Physic.Clamp(meanAnomalyRand, 1, 10);

                if (1 <= meanAnomalyRand && meanAnomalyRand <= 3) { meanDistance = randomSource.Next(1, 10) * 0.05f; OrbitalEccentricityRand -= 2; }//AU
                else if (4 <= meanAnomalyRand && meanAnomalyRand <= 6) { meanDistance = randomSource.Next(1, 10) * 0.5f; OrbitalEccentricityRand--; } //AU
                else if (7 <= meanAnomalyRand && meanAnomalyRand <= 8) meanDistance = randomSource.Next(1, 10) * 3; //AU
                else if (meanAnomalyRand == 9) meanDistance = randomSource.Next(1, 10) * 20; //AU
                else if (meanAnomalyRand == 10) meanDistance = randomSource.Next(1, 100) * 200; //AU

                //Orbital Eccentricity
                OrbitalEccentricityRand += randomSource.Next(1, 10);
                OrbitalEccentricityRand = Physic.Clamp(OrbitalEccentricityRand, 1, 10);
                int adder = randomSource.Next(1, 10);
                if (1 <= OrbitalEccentricityRand && OrbitalEccentricityRand <= 2) orbitalEccentricity = adder * 0.01f;
                else if (3 <= OrbitalEccentricityRand && OrbitalEccentricityRand <= 4) orbitalEccentricity = 0.01f + adder * 0.01f;
                else if (5 <= OrbitalEccentricityRand && OrbitalEccentricityRand <= 6) orbitalEccentricity = 0.02f + adder * 0.01f;
                else if (7 <= OrbitalEccentricityRand && OrbitalEccentricityRand <= 8) orbitalEccentricity = 0.03f + adder * 0.01f;
                else if (OrbitalEccentricityRand == 9) orbitalEccentricity = 0.04f + adder * 0.01f;
                else if (OrbitalEccentricityRand == 10) orbitalEccentricity = 0.05f + adder * 0.04f;

                periapsis = meanDistance * Physic.AstronomicUnit * (1 + orbitalEccentricity);
            }
            while (periapsis < A.Radius * Physic.SolarRadius + B.Radius * Physic.SolarRadius); //periapsis<radiusA+radiusB
            //TEST FOR EDGE CASES (ex radius<body radius)

            Orbit orbit;
            //Finaly, put together
            if (_testMode)
            {
                orbit = new Orbit(A, B, DateTime.Now, 0.0,1 * Physic.AstronomicUnit);
            }
            else
            {
                orbit = new Orbit(A, B, DateTime.Now, orbitalEccentricity, meanDistance * Physic.AstronomicUnit);
            }
            
            return orbit;
        }
        


    }
}
