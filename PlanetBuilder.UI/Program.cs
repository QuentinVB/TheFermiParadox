using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanetBuilder;

namespace PlanetBuilder.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("definig world");
            World world = new World();
            Console.WriteLine("creating a planet");
            Planet planet1 = new Planet(5.9736e24, "b612", world, 6378000,new PlanetaryModifer());
            Console.WriteLine("saving a planet");

            planet1.Save(BuilderSettings.Default.PlanetSave);
            string path = BuilderSettings.Default.PlanetSave + "b612" + ".xml";
            Console.WriteLine(" planet saved");

            Console.WriteLine(" retrieve from file :");
            Planet planet2 = (Planet)Planet.FromFile(path);
            Console.WriteLine(planet2);
            Console.ReadKey();
        }
    }
}
