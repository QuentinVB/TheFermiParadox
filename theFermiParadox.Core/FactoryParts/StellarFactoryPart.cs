using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using theFermiParadox.Core.Abstracts;
using theFermiParadox.Core.Models;
using theFermiParadox.DAL;

namespace theFermiParadox.Core
{
    public partial class SystemFactory //Stellar Part
    {
        public APhysicalObject GenerateStellarObject(ref StellarSystem stellar)
        {
            //define star class
            return GenerateStellarObject(ref stellar, randomSource.Next(1, 100));
        }

        public APhysicalObject GenerateStellarObject(ref StellarSystem stellarSystem, int stellarGenerationRand)
        {
            if (_testMode) return GenerateStar(stellarSystem, stellarGenerationRand);
            if (stellarGenerationRand < 100)
            {
                return GenerateStar(stellarSystem,stellarGenerationRand);
            }
            else
            //Could be B-class stars, giants, neutron stars,protostars or other rare stellar objects
            {
                GazCloud gazCloud = new GazCloud()
                {
                    Mass = 4f,
                };

                return gazCloud;
            }

        }



        

        public List<APhysicalObject> GenerateStellarCollection(ref StellarSystem stellarSystem)
        {
            return GenerateStellarCollection(ref stellarSystem,StarCount());
        }

        public List<APhysicalObject> GenerateStellarCollection(ref StellarSystem stellarSystem,int stellarAmount)
        {
            List<APhysicalObject> stellarList = new List<APhysicalObject>();

            //generate stellarList
            if (_testMode)
            {
                APhysicalObject @object = new Star("Sun", stellarSystem)
                {
                    StarClass = "G",
                    SizeCode = 2,
                    SpectralClass = 5,
                    Luminosity = 0.1,
                    SurfaceTemperature = 5000,
                    Radius = 1,
                    Mass = 1,
                };
                stellarSystem.Bodies.Add(@object as ABody);
                stellarList.Add(@object);

                @object = new Star("Nemesis", stellarSystem)
                {
                    StarClass = "D",
                    SizeCode = 2,
                    SpectralClass = 5,
                    Luminosity = 0.01,
                    SurfaceTemperature = 3000,
                    Radius = 0.2 ,
                    Mass = 0.2 ,
                };
                stellarSystem.Bodies.Add(@object as ABody);
                stellarList.Add(@object);
            }
            else
            {
                for (int i = 0; i < stellarAmount; i++)
                {

                    APhysicalObject @object = GenerateStellarObject(ref stellarSystem);
                    stellarSystem.Bodies.Add(@object as ABody);
                    stellarList.Add(@object);
                }
                
            }

            stellarList.Sort((x, y)=>
            {
                return y.Mass.CompareTo(x.Mass);
            });
            return stellarList;
        }
 
        public Star GenerateStar(StellarSystem stellarSystem, int starGenerationRand)
        {

            
            //star generation
            Star star;

            string starClass = "";
            int sizeCode = 0;

            int giantSize = randomSource.Next(1, 10) >= 7 ? 4 : 5;

            //manque O et B classes : case 0
            if (starGenerationRand == 1)
            {
                starClass = "A";
                sizeCode = giantSize;
            }
            if (2 <= starGenerationRand && starGenerationRand <= 4)
            {
                starClass = "F";
                sizeCode = giantSize;
            }
            if (5 <= starGenerationRand && starGenerationRand <= 12)
            {
                starClass = "G";
                sizeCode = giantSize;
            }
            if (13 <= starGenerationRand && starGenerationRand <= 26)
            {
                starClass = "K";
                sizeCode = 5;
            }
            if (27 <= starGenerationRand && starGenerationRand <= 36)//white dwarf, D for Degenerate
            {
                starClass = "D";
                sizeCode = 7;
            } 
            if (37 <= starGenerationRand && starGenerationRand <= 85)
            {
                starClass = "M";
                sizeCode = 5;
            }
            if (86 <= starGenerationRand && starGenerationRand <= 98)//brown dwarf LTY
            {
                starClass = "L";
            }
            if (starGenerationRand == 99)//Giants
            {
                int giantRand = randomSource.Next(1, 10);
                if (giantRand == 1) { starClass = "F"; sizeCode = 3; }
                if (giantRand == 2) { starClass = "G"; sizeCode = 3; }
                if (3 <= giantRand && giantRand <= 7) { starClass = "F"; sizeCode = 3; }
                if (giantRand > 8) { starClass = "K"; sizeCode = 4; }
            }
            
            //define spectralClass
            int spectralClass = (starClass == "K" && sizeCode == 4) ? 0 : randomSource.Next(0, 9);

            //White/Brown Dwarf case
            int dwarfGenerationRand = randomSource.Next(1, 10);
            if (starClass == "D" || starClass == "L")
            {
                star = new Star("unnamed", stellarSystem)
                {
                    StarClass = starClass,
                    SizeCode = 0,
                    SpectralClass = spectralClass
                };
            }
            else
            {
                //match from the table
                BasicStar basicStar = _basicStar.Find(
                    x => x.StarType == starClass
                    && x.SizeCode == sizeCode
                    && x.Random == spectralClass
                    );
                star = new Star("unnamed", stellarSystem)
                {
                    StarClass = starClass,
                    SizeCode = sizeCode,
                    SpectralClass = spectralClass,
                    Luminosity = basicStar.Luminosity,
                    SurfaceTemperature = basicStar.SurfaceTemperature,
                    Radius = basicStar.Radius,
                    Mass = basicStar.Mass,
                };
            }

           
            //Flares Star M3 to M9  1D10*50% periodicaly
            //TODO

            //subGiant randomisation
            if (sizeCode == 4)
            {
                int subGiantRand = randomSource.Next(1, 10);
                float rate = 0;
                switch (subGiantRand)
                {
                    case 3: rate = -0.1f; break;
                    case 4: rate = -0.2f; break;
                    case 5: rate = -0.3f; break;
                    case 6: rate = -0.4f; break;
                    case 7: rate = 0.1f; break;
                    case 8: rate = 0.2f; break;
                    case 9: rate = 0.3f; break;
                    case 10: rate = 0.4f; break;
                    default: rate = 0; break;
                }
                star.Mass += star.Mass * rate;
                star.Luminosity += star.Luminosity * rate * 2;
                star.Radius = (float)Math.Pow(star.Luminosity, 0.5 * (5800 / star.SurfaceTemperature) * (5800 / star.SurfaceTemperature));
            }

            //age
            int ageRandom = randomSource.Next(1, 10);

            //from table
            StarAge starAge = _systemAgeSource
                .FindAll(x => 
                            x.StarClass == starClass 
                            && x.SpectralClassMin <= star.SpectralClass 
                            && star.SpectralClass <= x.SpectralClassMax)
                .Find(x=> x.Random == ageRandom);

            star.Age = starAge.Age;
            star.LifeSpan = starAge.LifeSpan;

            //giant case & subgiant case
            if (sizeCode == 4 || sizeCode == 3)
            {
                star.Age = 10*star.Mass/star.Luminosity;
                star.Age += star.Age * ((sizeCode == 4)? 0.1f : 0.2f);
                star.LifeSpan = star.Age ;
            }
           
            //calculating temperature and luminosity of dwarves with age modifiers
            if (starClass == "L" || starClass == "D")
            {
                star.LifeSpan =  float.MaxValue ;
                DwarfStar dwarfData = 
                    (starClass == "D")?
                    _whitedwarfs.Find(x => x.Random == dwarfGenerationRand)
                    : _browndwarfs.Find(x => x.Random == dwarfGenerationRand);

                star.Mass = dwarfData.Mass;
                star.Radius = dwarfData.Radius;
                dwarfGenerationRand = Physic.Clamp(dwarfGenerationRand + starAge.TemperatureRollModifier, 1,10);
                dwarfData =
                    (starClass == "D") ?
                    _whitedwarfs.Find(x => x.Random == dwarfGenerationRand)
                    : _browndwarfs.Find(x => x.Random == dwarfGenerationRand);

                star.SurfaceTemperature = dwarfData.Temperature;

                star.Luminosity = Math.Pow(star.Radius, 2) * Math.Pow(star.SurfaceTemperature, 4) / Math.Pow(5800,4);
            }

            star.Luminosity += star.Luminosity * starAge.LuminosityModifier;

            return star??throw new InvalidOperationException();   
        }
    
        private int StarCount()
        {
            int count = 1;
            int adder = 0;
            do
            {
                if (randomSource.Next(0, 10) >= 6)
                {
                    count++;
                }
                else
                {
                    break;
                }
                adder++;
            } while (adder != 10);
            return count;
        }
    }
}
