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
            stellarSystem.SystemAge = systemAge;

            return stellarSystem;
        }
        
        
        private Orbit ForgeOrbit(ABody A, ABody B, double systemAge)
        {
           

            int OrbitalEccentricityRand = 0;

            //Mean Separation p10
            int meanSeparationRand = randomSource.Next(1, 10);
            if (systemAge > 5) meanSeparationRand++;
            if (systemAge < 1) meanSeparationRand--;

            meanSeparationRand = PhysicHelpers.Clamp(meanSeparationRand, 1, 10);
            float meanSeparation = 0;
            if (1 <= meanSeparationRand && meanSeparationRand <= 3) {meanSeparation = randomSource.Next(1, 10) * 0.05f; OrbitalEccentricityRand -= 2; }//AU
            if (4 <= meanSeparationRand && meanSeparationRand <= 6) { meanSeparation = randomSource.Next(1, 10) * 0.5f; OrbitalEccentricityRand--; } //AU
            if (7 <= meanSeparationRand && meanSeparationRand <= 8) meanSeparation = randomSource.Next(1,10)* 3; //AU
            if (meanSeparationRand== 9) meanSeparation = randomSource.Next(1,10)* 20; //AU
            if (meanSeparationRand== 10) meanSeparation = randomSource.Next(1,100)* 200; //AU

            //Orbital Eccentricity
            float OrbitalEccentricity = 0;
            OrbitalEccentricityRand += randomSource.Next(1, 10);
            OrbitalEccentricityRand = PhysicHelpers.Clamp(OrbitalEccentricityRand,1, 10);
            int adder = randomSource.Next(1, 10);
            if (1 <= OrbitalEccentricityRand && OrbitalEccentricityRand <= 2) OrbitalEccentricity= adder*0.01f;
            if (3 <= OrbitalEccentricityRand && OrbitalEccentricityRand <= 4) OrbitalEccentricity= 0.01f + adder *0.01f;
            if (5 <= OrbitalEccentricityRand && OrbitalEccentricityRand <= 6) OrbitalEccentricity= 0.02f + adder *0.01f;
            if (7 <= OrbitalEccentricityRand && OrbitalEccentricityRand <= 8) OrbitalEccentricity= 0.03f + adder *0.01f;
            if (OrbitalEccentricityRand == 9) OrbitalEccentricity = 0.04f + adder * 0.01f;
            if (OrbitalEccentricityRand == 10) OrbitalEccentricity = 0.05f + adder * 0.04f;

            //apoapsis
            float apoapsis = meanSeparation * (1 - OrbitalEccentricity);
            //periapsis
            float periapsis = meanSeparation * (1 + OrbitalEccentricity);

            //Orbital Period
            //if virtual => take mass of childs
            //TimeSpan OrbitalPeriod = new TimeSpan((long)(Math.Pow(Math.Pow(meanSeparation, 3) / (A.Mass + B.Mass), 0.5)));


            //Finaly, put together
            Orbit orbit = new Orbit(A, B)
            {
                Apoapsis = apoapsis,
                Periapsis = periapsis,
                Eccentricity = OrbitalEccentricity
            };


            return orbit;
        }
        


    }
}
