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

            randomSource = new Random();
        }

        private string GetSystemName()
        {
            if(_testMode)
            {
                return "Sol";
            }

            return $"{Physic.GREEKALPHABET[randomSource.Next(0, Physic.GREEKALPHABET.Length - 1)]} {_starNames[randomSource.Next(0, _starNames.Length-1)].Name}";
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
            int abudance = randomSource.Next(1, 20) + (int)systemAge;
            int abudanceModifier = 0;
            if(3 <= abudance && abudance <= 9) abudanceModifier = 2;
            else if(10 <= abudance && abudance <= 12) abudanceModifier = 1;
            else if (13 <= abudance && abudance <= 18) abudanceModifier = 0;
            else if (19 <= abudance && abudance <= 21) abudanceModifier = -1;
            else if (21 <= abudance ) abudanceModifier = -3;

            //Building Orbits

            //ring orbits, as substitute
            /*
            for (int i = 1; i < stellarCollection.Count; i++)
            {
                Orbit orbit = ForgeOrbit(stellarCollection[0], stellarCollection[i], systemAge,i-1);
                stellarSystem.Orbits.Add(orbit);
            }*/

            ABody ForgeBinaryOrbit(APhysicalObject bodyA, APhysicalObject bodyB,int offset)
            {
                if (bodyA.Mass > bodyB.Mass * 9.9)//B mass is significatifly 10 times less than A mass
                {
                    Orbit orbit = ForgeOrbit(stellarCollection[0], stellarCollection[1], systemAge);
                    stellarSystem.Orbits.Add(orbit);

                    return orbit.MainBody;
                }
                else// B mass is close to A mass : barycenter orbit
                {
                    Barycenter barycenter = new Barycenter(stellarSystem, stellarCollection[0], stellarCollection[1])
                    {
                        Name = "Barycenter"
                    };
                    stellarSystem.Add(barycenter);

                    Orbit orbit = ForgeOrbit(barycenter, stellarCollection[0], systemAge);
                    stellarSystem.Orbits.Add(orbit);

                    Orbit orbit2 = ForgeOrbit(barycenter, stellarCollection[1], systemAge);
                    stellarSystem.Orbits.Add(orbit2);

                    return barycenter;
                }
            }

            //multiple star system
            //By Convention name are A,B,C... in the decreasing mass order

            if (stellarCollection.Count>1)
            {
                ABody[] roots = new ABody[stellarCollection.Count / 2];
                int index=0;
                
                //creating binary couple
                for (int i = 0; i < stellarCollection.Count-1; i += 2)
                {
                    //get 2 first
                    APhysicalObject bodyA = stellarCollection[i];
                    APhysicalObject bodyB = stellarCollection[i + 1];
                    roots[index]= (ABody)ForgeBinaryOrbit(bodyA, bodyB, 1 - i);
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
                    throw new NotImplementedException();
                }

                if (stellarCollection.Count % 2 > 0)//odd result, there is a lonely star to add
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                //A single lonely star
                stellarSystem.PhysicalObjectRoot = stellarCollection[0];

            }
            return stellarSystem;
        }   
    }
}
