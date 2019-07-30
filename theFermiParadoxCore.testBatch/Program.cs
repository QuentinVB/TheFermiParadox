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

            SystemFactory systemFactory = new SystemFactory(true);


            StellarSystem stellarSystem = systemFactory.GetStellarSystem(2);

            TimeSpan timeOffset = new TimeSpan(1, 0, 0, 0, 0);
            //Printer<Orbit>.PrintHeader("orbitRecord.csv", stellarSystem.Orbits[0]);

            List<Orbit> orbitalStates = new List<Orbit>();

            for (int i = 0; i < 50; i++)
            {
                stellarSystem.Orbits[0].UpdateTime(timeOffset);
                orbitalStates.Add((Orbit)stellarSystem.Orbits[0].Clone());
            }
            Printer<Orbit>.PrintTable("orbitRecord.csv", orbitalStates);


            Console.WriteLine("Finished...");


            Console.ReadLine();
        }
    }
}
