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

            StellarFactory stellarFactory = new StellarFactory();

            StellarSystem system = new StellarSystem();

            Random randSource = new Random();

            Star star = stellarFactory.GenerateStar(system, randSource.Next(0,100));
            Console.WriteLine(star);
            /*
            StellarFactory stellarFactory = new StellarFactory();
            List<AStellarObject> stellarList = stellarFactory.GenerateStellarCollection(    );
            //to show
            foreach (AStellarObject stellarObject in stellarList)
            {
                Console.WriteLine(stellarObject);
            }
            */
            Console.ReadLine();
        }
    }
}
