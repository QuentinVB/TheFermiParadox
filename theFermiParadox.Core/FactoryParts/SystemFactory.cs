using System;
using System.Collections.Generic;
using System.Text;
using theFermiParadox.Core.Abstracts;
using theFermiParadox.Core.Models;
using theFermiParadox.DAL;

namespace theFermiParadox.Core
{
    public partial class SystemFactory
    {
        Random randomSource;

        readonly List<StarGeneration> _starGeneration;
        readonly List<BasicStar> _basicStar;
        readonly List<StarAge> _systemAgeSource;
        readonly List<DwarfStar> _whitedwarfs;
        readonly List<DwarfStar> _browndwarfs;

        public SystemFactory()
        {
            _starGeneration = Loader<StarGeneration>.LoadTable("starGeneration.csv");
            _basicStar = Loader<BasicStar>.LoadTable("basicStar.csv");
            _systemAgeSource = Loader<StarAge>.LoadTable("systemAge.csv");
            _whitedwarfs = Loader<DwarfStar>.LoadTable("whitedwarf.csv");
            _browndwarfs = Loader<DwarfStar>.LoadTable("browndwarf.csv");

            randomSource = new Random();
        }

        public StellarSystem GetStellarSystem()
        {
            return GetStellarSystem(0);
        }
        public StellarSystem GetStellarSystem(int starCount)
        {
            
            StellarSystem stellarSystem = new StellarSystem();

            //generate a collection of StarLike Objects ordered by mass
            List<APhysicalObject> stellarCollection;
            if (starCount==0)
            {
                stellarCollection = GenerateStellarCollection(ref stellarSystem);
            }
            else
            {
                stellarCollection = GenerateStellarCollection(ref stellarSystem, starCount);
            }

            //system age (the smallest)
            double systemAge = double.MaxValue;
            foreach (APhysicalObject stellarObject in stellarCollection)
            {
                if (stellarObject is Star star)
                {
                    if (star.Age < systemAge) systemAge = star.Age;
                }                
            }
            stellarSystem.SystemAge = systemAge;


            //TODO : set age modification for each stars based from table

            //determining abudance factor
            int abudance = randomSource.Next(1, 20) + (int)systemAge;
            int abudanceModifier = 0;
            if(3 <= abudance && abudance <= 9) abudanceModifier = 2;
            else if(10 <= abudance && abudance <= 12) abudanceModifier = 1;
            else if (13 <= abudance && abudance <= 18) abudanceModifier = 0;
            else if (19 <= abudance && abudance <= 21) abudanceModifier = -1;
            else if (21 <= abudance ) abudanceModifier = -3;

            //Building Orbits
            //By Convention name are A,B,C... in the decreasing mass order
            if(stellarCollection.Count ==2)
            {
                //B orbiting A
                Orbit orbit = ForgeOrbit(stellarCollection[0], stellarCollection[1], systemAge);
                stellarSystem.Orbits.Add(orbit);
                //orbit.MainBody.ChildOrbit.Add(orbit);
                //orbit.Body.ParentOrbit = orbit;
            }
            else if (stellarCollection.Count == 3)
            {
                //triple case for C : around A, around B, Around AB
            }
            else if (stellarCollection.Count == 4)
            {
                //4 case for D :around A, around AB with C,  Around ABC
            }
            else if (stellarCollection.Count == 5)
            {
                //3 case for D : around AB and CD,  Around AB , around CD
            }
            else if (stellarCollection.Count == 6)
            {
                //3 case for D : around AB and CD,  Around AB , around CD
            }
            else
            {
                //orbit on rings
            }



            //set the age limitation factor
            //calculating orbits

            //
           
            return stellarSystem;
        }
        
        
        private Orbit ForgeOrbit(IOrbitable A, IOrbitable B, double systemAge)
        {
            double periapsis;
            double meanAnomaly = 0;
            double orbitalEccentricity = 0;

            do
            {



                int OrbitalEccentricityRand = 0;

                //Mean Separation p10
                int meanAnomalyRand = randomSource.Next(1, 10);
                if (systemAge > 5) meanAnomalyRand++;
                else if (systemAge < 1) meanAnomalyRand--;

                meanAnomalyRand = PhysicHelpers.Clamp(meanAnomalyRand, 1, 10);
                if (1 <= meanAnomalyRand && meanAnomalyRand <= 3) { meanAnomaly = randomSource.Next(1, 10) * 0.05f; OrbitalEccentricityRand -= 2; }//AU
                else if (4 <= meanAnomalyRand && meanAnomalyRand <= 6) { meanAnomaly = randomSource.Next(1, 10) * 0.5f; OrbitalEccentricityRand--; } //AU
                else if (7 <= meanAnomalyRand && meanAnomalyRand <= 8) meanAnomaly = randomSource.Next(1, 10) * 3; //AU
                else if (meanAnomalyRand == 9) meanAnomaly = randomSource.Next(1, 10) * 20; //AU
                else if (meanAnomalyRand == 10) meanAnomaly = randomSource.Next(1, 100) * 200; //AU

                //Orbital Eccentricity
                OrbitalEccentricityRand += randomSource.Next(1, 10);
                OrbitalEccentricityRand = PhysicHelpers.Clamp(OrbitalEccentricityRand, 1, 10);
                int adder = randomSource.Next(1, 10);
                if (1 <= OrbitalEccentricityRand && OrbitalEccentricityRand <= 2) orbitalEccentricity = adder * 0.01f;
                else if (3 <= OrbitalEccentricityRand && OrbitalEccentricityRand <= 4) orbitalEccentricity = 0.01f + adder * 0.01f;
                else if (5 <= OrbitalEccentricityRand && OrbitalEccentricityRand <= 6) orbitalEccentricity = 0.02f + adder * 0.01f;
                else if (7 <= OrbitalEccentricityRand && OrbitalEccentricityRand <= 8) orbitalEccentricity = 0.03f + adder * 0.01f;
                else if (OrbitalEccentricityRand == 9) orbitalEccentricity = 0.04f + adder * 0.01f;
                else if (OrbitalEccentricityRand == 10) orbitalEccentricity = 0.05f + adder * 0.04f;

                periapsis = meanAnomaly * (1 + orbitalEccentricity);
            }
            while (periapsis > A.Radius +B.Radius); //periapsis<radiusA+radiusB
            //TEST FOR EDGE CASES (ex radius<body radius)


            //Finaly, put together
            Orbit orbit = new Orbit(A, B, DateTime.Now);
            orbit.Initialize(orbitalEccentricity, meanAnomaly);


            return orbit;
        }
        


    }
}
