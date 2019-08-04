using System;
using System.Collections.Generic;
using System.Text;
using theFermiParadox.Core.Abstracts;
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

        readonly bool _testMode;

        /// <summary>
        /// The factory that create stellar system
        /// </summary>
        /// <param name="testMode">if test mode, generate a simple binary star system with a Yellow sun and an red dwarf orbiting circulary</param>
        public SystemFactory(bool testMode=false)
        {
            _testMode = testMode;
            //load ressources (very inelegant)
            if(!_testMode)
            {
                _starGeneration = Loader<StarGeneration>.LoadTable("starGeneration.csv");
                _basicStar = Loader<BasicStar>.LoadTable("basicStar.csv");
                _systemAgeSource = Loader<StarAge>.LoadTable("systemAge.csv");
                _whitedwarfs = Loader<DwarfStar>.LoadTable("whitedwarf.csv");
                _browndwarfs = Loader<DwarfStar>.LoadTable("browndwarf.csv");
            }

            randomSource = new Random();
        }

        public StellarSystem GetStellarSystem()
        {
            return GetStellarSystem(0);
        }
        public StellarSystem GetStellarSystem(int starCount)
        {
            if (_testMode) starCount = 2;

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

            //ring orbits, as substitute
            for (int i = 1; i < stellarCollection.Count; i++)
            {
                Orbit orbit = ForgeOrbit(stellarCollection[0], stellarCollection[i], systemAge);
                stellarSystem.Orbits.Add(orbit);
            }

            //WARNING : EXTREMLY NOT DRY ! Algorithmic solution needed
            /*
            //By Convention name are A,B,C... in the decreasing mass order
            if(stellarCollection.Count ==2)
            {
                //B orbiting A
                Orbit orbit = ForgeOrbit(stellarCollection[0], stellarCollection[1], systemAge);
                stellarSystem.Orbits.Add(orbit);
            }
            else if (stellarCollection.Count == 3)
            {
                //triple case for C : around A, around B, Around AB

                APhysicalObject A = stellarCollection[0];
                APhysicalObject B = stellarCollection[1];
                APhysicalObject C = stellarCollection[2];

                int dice = randomSource.Next(0,2);
                if (dice==0)
                {
                    //Create AB
                    Orbit orbit = ForgeOrbit(stellarCollection[0], stellarCollection[1], systemAge);
                    stellarSystem.Orbits.Add(orbit);

                    // around A
                    Orbit orbit2 = ForgeOrbit(stellarCollection[0], stellarCollection[2], systemAge);
                    stellarSystem.Orbits.Add(orbit2);

                } //around B
                else if (dice == 1)
                {
                    //Create AB
                    Orbit orbit = ForgeOrbit(stellarCollection[0], stellarCollection[1], systemAge);
                    stellarSystem.Orbits.Add(orbit);

                    // around A
                    Orbit orbit2 = ForgeOrbit(stellarCollection[1], stellarCollection[2], systemAge);
                    stellarSystem.Orbits.Add(orbit2);
                }
                else //Around AB
                {
                    //Create AB

                    //forge barycentric orbit between A,B and a barycenter
                    //forge orbit of C around barycenter

                    //Orbit orbit = ForgeOrbit(stellarCollection[0], stellarCollection[1], systemAge);
                    //stellarSystem.Orbits.Add(orbit);
                }

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
            */

            stellarSystem.PhysicalObjectRoot = stellarCollection[0];
            //set the age limitation factor

            //

            return stellarSystem;
        }   
    }
}
