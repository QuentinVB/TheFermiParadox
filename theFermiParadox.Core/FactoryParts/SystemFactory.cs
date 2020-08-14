using System;
using System.Collections.Generic;
using System.Text;
using theFermiParadox.Core.Abstracts;
using theFermiParadox.Core.Utilities;
using theFermiParadox.DAL;

namespace theFermiParadox.Core
{
    public partial class SystemFactory
    {
        RandomF randomSource;

        readonly List<StarGeneration> _starGeneration;
        readonly List<BasicStar> _basicStar;
        readonly List<StarAge> _systemAgeSource;
        readonly List<DwarfStar> _whitedwarfs;
        readonly List<DwarfStar> _browndwarfs;
        readonly StarName[] _starNames;

        readonly bool _testMode;

        /// <summary>
        /// The factory that create stellar system
        /// </summary>
        /// <param name="testMode">if test mode, generate a simple binary star system with a Yellow sun and an red dwarf orbiting circulary</param>
        public SystemFactory(bool testMode=false)
        {
            _testMode = testMode;
            //load ressources (very inelegant, next time async)
            if(!_testMode)
            {
                _starGeneration = Loader<StarGeneration>.LoadTable("starGeneration.csv");
                _basicStar = Loader<BasicStar>.LoadTable("basicStar.csv");
                _systemAgeSource = Loader<StarAge>.LoadTable("systemAge.csv");
                _whitedwarfs = Loader<DwarfStar>.LoadTable("whitedwarf.csv");
                _browndwarfs = Loader<DwarfStar>.LoadTable("browndwarf.csv");
                _starNames = Loader<StarName>.LoadTable("constellationNames.csv").ToArray();
            }

            randomSource = new RandomF();
        }

        private string GetSystemName()
        {
            if(_testMode) return "Sol";


            return $"{Physic.GREEKALPHABET[randomSource.NextInclusive(0, Physic.GREEKALPHABET.Length - 1)]} {_starNames[randomSource.NextInclusive(0, _starNames.Length-1)].Name}";
        }

        public StellarSystem GetStellarSystem()
        {
            return GetStellarSystem(0);
        }

        public StellarSystem GetStellarSystem(int starCount)
        {
            if (_testMode) starCount = 2;

            StellarSystem stellarSystem = new StellarSystem(GetSystemName());

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
            int abudance = randomSource.NextInclusive(1, 20) + (int)systemAge;
            int abudanceModifier = 0;
            if(3 <= abudance && abudance <= 9) abudanceModifier = 2;
            else if(10 <= abudance && abudance <= 12) abudanceModifier = 1;
            else if (13 <= abudance && abudance <= 18) abudanceModifier = 0;
            else if (19 <= abudance && abudance <= 21) abudanceModifier = -1;
            else if (21 <= abudance ) abudanceModifier = -3;

            //Building Orbits

            //ring orbits, as temp substitute
            /*
            for (int i = 1; i < stellarCollection.Count; i++)
            {
                Orbit orbit = ForgeOrbit(stellarCollection[0], stellarCollection[i], systemAge,i-1);
                stellarSystem.Orbits.Add(orbit);
            }*/

            

            //manage multiple star system
            //By Convention name are A,B,C... in the decreasing mass order

            if (stellarCollection.Count>1)
            {
                IOrbitable[] roots = new IOrbitable[(int)Math.Floor( stellarCollection.Count / 2.0)];
                int index=0;
                
                //creating binary couple
                for (int i = 0; i < stellarCollection.Count-1; i += 2)
                {
                    //get 2 first
                    APhysicalObject bodyA = stellarCollection[i];
                    APhysicalObject bodyB = stellarCollection[i + 1];
                    roots[index]= ForgeBinaryOrbit(ref stellarSystem, bodyA, bodyB, systemAge);
                    index++;
                }

                if (index==1)
                {
                    //only one pair , get the root (barycenter or the most massive star)
                    stellarSystem.PhysicalObjectRoot = roots[0];

                }
                else
                {
                    //THAR BE DRAGONZ
                    //many pairs

                    IOrbitable rootPair = roots[0];
                    int offset = 0;
                    for (int i = 1; i < roots.Length-1; i++)
                    {
                        Orbit orbit = ForgeOrbit(rootPair, roots[i],systemAge,i);
                        stellarSystem.Orbits.Add(orbit);
                        offset++;
                    }

                    if (stellarCollection.Count % 2 > 0)//odd result, there is a lonely star to add
                    {
                        Orbit orbit = ForgeOrbit(rootPair, roots[roots.Length - 1], systemAge, offset+1);
                        stellarSystem.Orbits.Add(orbit);
                    }

                    stellarSystem.PhysicalObjectRoot = roots[0];

                }

            }
            else
            {
                //A single lonely star, sad, but simple
                stellarSystem.PhysicalObjectRoot = stellarCollection[0];

            }
            return stellarSystem;
        }   
    }
}
