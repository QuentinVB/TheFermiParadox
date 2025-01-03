using System;
using theFermiParadox.Core;

namespace theFermiParadox.ManualTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading...");

            SystemFactory systemFactory = new SystemFactory();

            StellarSystem stellarSystem = systemFactory.GetStellarSystem(3);

            /*
            foreach (ABody body in stellarSystem.Bodies)
            {
                Console.WriteLine(body);
            }
            */

            Console.WriteLine(stellarSystem);

            /*
            TimeSpan timeOffset = new TimeSpan(20, 0, 0, 0, 0);
            //Printer<Orbit>.PrintHeader("orbitRecord.csv", stellarSystem.Orbits[0]);

            List<Orbit> orbitalStates = new List<Orbit>();

            int max = 3000;
            for (int i = 0; i < max; i++)
            {
                stellarSystem.Orbits[0].UpdateTime(timeOffset);
                orbitalStates.Add((Orbit)stellarSystem.Orbits[0].Clone());
                Console.WriteLine($"{i}/{max}");
            }
            Printer<Orbit>.PrintTable("orbitRecord.csv", orbitalStates);
            Console.WriteLine($"{stellarSystem.Orbits[0].OrbitalPeriod / (24 * 60 * 60 * 365)} years");
            */
            Console.WriteLine("Finished...");

            Console.ReadLine();
        }
    }
}
