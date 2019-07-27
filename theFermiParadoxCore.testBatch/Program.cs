using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using theFermiParadox.Core;
using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.ManualTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading...");

            SystemFactory systemFactory = new SystemFactory();

            StellarSystem system = new StellarSystem();

            Random randSource = new Random();

            //Star star = systemFactory.GenerateStar(system, randSource.Next(0,100));
            //Console.WriteLine(star);
            StellarSystem stellarSystem = systemFactory.GetStellarSystem(2);
            //List<APhysicalObject> stellarList = systemFactory.GenerateStellarCollection(ref stellarSystem);
            //to show
            foreach (APhysicalObject stellarObject in stellarSystem.Bodies)
            {
                Console.WriteLine(stellarObject);
            }
            
            Console.ReadLine();
        }
    }
}
