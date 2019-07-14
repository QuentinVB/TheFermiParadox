using System;
using System.Collections.Generic;
using System.Text;
using theFermiParadox.Core.Abstracts;


namespace theFermiParadox.Core
{
    class SystemFactory
    {
        StellarFactory _stellarFactory;
        Random randomSource;
        public SystemFactory(StellarFactory stellarFactory)
        {
            _stellarFactory = stellarFactory;
            randomSource = new Random();
        }
        /*
        public StellarSystem GetStellarSystem()
        {
            StellarSystem stellarSystem = new StellarSystem();

            List<AStellarObject> stellarCollection = _stellarFactory.GenerateStellarCollection(stellarSystem);
            
            //system age (the smallest)
            float systemAge = 0;
            foreach (AStellarObject stellarObject in stellarCollection)
            {
                if (stellarObject.GetType().Equals(typeof(Star)))
                {
                    if ((stellarObject as Star).Age < systemAge) systemAge = (stellarObject as Star).Age;
                }                
            }

            //set age for each stars based from table

            //determining abudance factor
            int abudance = randomSource.Next(1, 20) + (int)systemAge;
            int abudanceModifier = 0;
            if(3 <= abudance && abudance <= 9) abudanceModifier = 2;
            if(10 <= abudance && abudance <= 12) abudanceModifier = 1;
            if(13 <= abudance && abudance <= 18) abudanceModifier = 0;
            if(19 <= abudance && abudance <= 21) abudanceModifier = -1;
            if(21 <= abudance ) abudanceModifier = -3;

            //multiple star case
            if(stellarCollection.Count ==2)
            {
                //B orbiting A

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
        */
        /*
        private IOrbit forgeOrbit(ABody A, ABody B, double systemAge)
        {
            int OrbitalEccentricityRand = 0;

            //Mean Separation p10
            int meanSeparationRand = randomSource.Next(1, 10);
            if (systemAge > 5) meanSeparationRand++;
            if (systemAge < 1) meanSeparationRand--;

            meanSeparationRand = PhysicHelpers.Clamp(meanSeparationRand, 1, 10);
            float meanSeparation = 0;
            if (1 <= meanSeparationRand && meanSeparationRand <= 3) {meanSeparation = randomSource.Next(1, 10) * 0.05; OrbitalEccentricityRand -= 2; }//AU
            if (4 <= meanSeparationRand && meanSeparationRand <= 6) { meanSeparation = randomSource.Next(1, 10) * 0.5; OrbitalEccentricityRand--; } //AU
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
            TimeSpan OrbitalPeriod = new TimeSpan((long)(Math.Pow(Math.Pow(meanSeparation, 3) / (A.Mass + B.Mass), 0.5)));
                


        }*/



    }
}
