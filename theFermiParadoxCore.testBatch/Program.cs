using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using theFermiParadox.Core;
using theFermiParadox.Core.Abstracts;
using theFermiParadox.DAL;

namespace theFermiParadox.ManualTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading...");

            SystemFactory systemFactory = new SystemFactory();


            StellarSystem stellarSystem = systemFactory.GetStellarSystem(2);

            TimeSpan timeOffset = new TimeSpan(1, 0, 0, 0, 0);
            Printer<Orbit>.PrintHeader("orbitRecord.csv", stellarSystem.Orbits[0]);
            
            for (int i = 0; i < 10; i++)
            {
                stellarSystem.Orbits[0].UpdateTime(timeOffset);
                Printer<Orbit>.PrintRecord("orbitRecord.csv", stellarSystem.Orbits[0]);
            }

    


            Console.WriteLine("Finished...");


            Console.ReadLine();
        }
    }
}
