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
    public class StellarFactory
    {
        Random randomSource = new Random();

        readonly List<StarGeneration> _starGeneration;
        readonly List<BasicStar> _basicStar;
        readonly List<StarAge> _systemAgeSource;
        readonly List<DwarfStar> _whitedwarfs;
        readonly List<DwarfStar> _browndwarfs;

        public StellarFactory()
        {
            _starGeneration= Loader<StarGeneration>.LoadTable("starGeneration.csv");
            _basicStar = Loader<BasicStar>.LoadTable("basicStar.csv");
            _systemAgeSource = Loader<StarAge>.LoadTable("systemAge.csv");
            _whitedwarfs = Loader<DwarfStar>.LoadTable("whitedwarf.csv");
            _browndwarfs = Loader<DwarfStar>.LoadTable("browndwarf.csv");     
        }


        public List<APhysicalObject> GenerateStellarCollection(StellarSystem stellarSystem)
        {
            return GenerateStellarCollection(stellarSystem,StarCount());
        }

        public List<APhysicalObject> GenerateStellarCollection(StellarSystem stellarSystem,int stellarAmount)
        {
            List<APhysicalObject> stellarList = new List<APhysicalObject>();
            
            Console.WriteLine($"star system count : {stellarAmount}");
            //generate stellarList
            for (int i = 0; i < stellarAmount; i++)
            {
                stellarList.Add(GenerateStellarObject(stellarSystem));
            }
            stellarList.Sort(delegate (APhysicalObject x, APhysicalObject y)
            {
                return x.Mass.CompareTo(y.Mass);
            });
            stellarList.Reverse();
            return stellarList;
        }


        public APhysicalObject GenerateStellarObject(StellarSystem stellar)
        {
            //define star class
            return GenerateStellarObject(stellar,randomSource.Next(1, 100));
        }
        public APhysicalObject GenerateStellarObject(StellarSystem stellarSystem,int stellarGenerationRand)
        {
            if(stellarGenerationRand <100)
            {
                throw new NotImplementedException(); //GenerateStar(stellarSystem,stellarGenerationRand);
            }
            else
            //Could be B-class stars, giants, neutron stars,protostars or other rare stellar objects
            {
                GazCloud gazCloud = new GazCloud()
                {
                    Mass = 4f,
                };

                return gazCloud;
                //starClass = "Special";
            }
            //return new GazCloud();

        }

        /*
        public Star GenerateStar(StellarSystem stellarSystem, int starGenerationRand)
        {


            //star generation
            string starClass = "";
            string sizeCode = "";
            //manque O et B classes : case 0
            if (starGenerationRand == 1)
            { starClass = "A"; sizeCode = randomSource.Next(1, 10) >= 7 ? "IV" : "V"; }
            if (2 <= starGenerationRand && starGenerationRand <= 4)
            { starClass = "F"; sizeCode = randomSource.Next(1, 10) >= 7 ? "IV" : "V"; }
            if (5 <= starGenerationRand && starGenerationRand <= 12)
            { starClass = "G"; sizeCode = randomSource.Next(1, 10) >= 7 ? "IV" : "V"; }
            if (13 <= starGenerationRand && starGenerationRand <= 26)
            { starClass = "K"; sizeCode = "V"; }
            if (27 <= starGenerationRand && starGenerationRand <= 36)
            { starClass = "D"; sizeCode = "VII"; } //white dwarf, D for Degenerate
            if (37 <= starGenerationRand && starGenerationRand <= 85)
            { starClass = "M"; sizeCode = "V"; }
            if (86 <= starGenerationRand && starGenerationRand <= 98)
            { starClass = "L"; }//brown dwarf LTY
            if (starGenerationRand == 99)//Giants
            {
                int giantRand = randomSource.Next(1, 10);
                if (giantRand == 1) { starClass = "F"; sizeCode = "III"; }
                if (giantRand == 2) { starClass = "G"; sizeCode = "III"; }
                if (3 <= giantRand && giantRand <= 7) { starClass = "F"; sizeCode = "III"; }
                if (giantRand > 8) { starClass = "K"; sizeCode = "IV"; }
            }
            
            //define spectralClass
            int spectralClass = (starClass == "K" && sizeCode == "IV") ? 0 : randomSource.Next(0, 9);

            //match from the table
            Star star = _starSource.Stars.Find(x => x.StarClass == starClass && x.SizeCode == sizeCode && x.SpectralClass == spectralClass);

            //White/Brown Dwarf case
            int dwarfGenerationRand = randomSource.Next(1, 10);
            if (starClass == "D" || starClass == "L")
            {
                star = new Star(stellarSystem, new EmptyOrbit())
                {
                    StarClass = starClass,
                    SizeCode = "-",
                    SpectralClass = spectralClass
                };
            }
            /*
            if (star == null)
            {
                star = new Star
                {
                    StarClass = starClass,
                    Radius = 1,
                    SurfaceTemperature = 0
                };
            }

            //Flares Star M3 to M9  1D10*50% periodicaly
            //TODO

            //subGiant randomisation
            if (sizeCode == "IV")
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
            StarAge starAge = _systemAgeSource.StarAges
                .FindAll(x => 
                            x.StarClass == starClass 
                            && x.SpectralClassMin <= star.SpectralClass 
                            && star.SpectralClass <= x.SpectralClassMax)
                .Find(x=> x.random == ageRandom);

            star.Age = starAge.Age;
            star.LifeSpan = starAge.LifeSpan;

            //giant case & subgiant case
            if (sizeCode == "IV" || sizeCode == "III")
            {
                star.Age = 10*star.Mass/star.Luminosity;
                star.Age += star.Age * ((sizeCode == "IV")? 0.1f : 0.2f);
                star.LifeSpan = star.Age ;
            }
           
            //calculating temperature and luminosity of dwarves with age modifiers
            if (starClass == "L" || starClass == "D")
            {
                star.LifeSpan =  float.MaxValue ;
                DwarfStar dwarfData = 
                    (starClass == "D")?
                    _whitedwarfsources.DwarfStars.Find(x => x.Random == dwarfGenerationRand)
                    : _browndwarfsources.DwarfStars.Find(x => x.Random == dwarfGenerationRand);

                star.Mass = dwarfData.Mass;
                star.Radius = dwarfData.Radius;
                dwarfGenerationRand = PhysicHelpers.Clamp(dwarfGenerationRand + starAge.TemperatureRollModifier, 1,10);
                dwarfData =
                    (starClass == "D") ?
                    _whitedwarfsources.DwarfStars.Find(x => x.Random == dwarfGenerationRand)
                    : _browndwarfsources.DwarfStars.Find(x => x.Random == dwarfGenerationRand);

                star.SurfaceTemperature = dwarfData.Temperature;

                star.Luminosity = (float)Math.Pow(star.Radius, 2) * (float)Math.Pow(star.SurfaceTemperature, 4) / 5800 * 5800 * 5800 * 5800;
            }

            star.Luminosity += star.Luminosity * starAge.LuminosityModifier;

            return star;   
        }
    */
        private int StarCount()
        {
            Random randomSource = new Random();
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
