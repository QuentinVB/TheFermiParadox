using System.Collections.Generic;
using theFermiParadox.Core.Abstracts;
using System.Linq;
using Helpers;
using System;

namespace theFermiParadox.Core
{
    public class StellarSystem
    {
        List<ABody> _bodies;
        List<Orbit> _orbits;
        ABody _objectRoot;
        double _systemAge;

        int starCount = 0;
        int planetCount = 0;

        private readonly string _name;
        private readonly Guid _uuid;

        public StellarSystem(string name)
        {
            _uuid = Guid.NewGuid();
            _name = name;

            _bodies = new List<ABody>();
            _orbits = new List<Orbit>();
        }
        
        public void Add(ABody body)
        {
            _bodies.Add(body);

            if (body is Star) starCount ++;
            if (body is Planet) planetCount++;

            //keeping it sorted by mass ?
        }

        public double Mass
        {
            get {
                double sum = 0;
                foreach (ABody body in _bodies)
                {
                    if (body is APhysicalObject @object) sum += @object.Mass;
                }
                return sum;
            }
        }

        /// <summary>
        /// position from barycenter calculator ?
        /// </summary>
        public Vector3 Barycenter { get; }
        /// <summary>
        /// The first object of the tree
        /// </summary>
        public ABody PhysicalObjectRoot { get => _objectRoot; internal set => _objectRoot = value; }
        /// <summary>
        /// Gets the index of the body in the system.
        /// </summary>
        /// <param name="aBody">a body.</param>
        /// <returns>the index</returns>
        internal int GetIndexOf(ABody aBody)
        {
            int idx=0;
            foreach (ABody  body in _bodies)
            {
                if(body is IStellar stellarObject)
                {
                    idx++;
                    if (body.Uuid == aBody.Uuid) return idx;
                }
            }
            throw new KeyNotFoundException("the body is not in the body collection of the system");
        }

        /// <summary>
        /// list of bodies inside the stellar system
        /// </summary>
        public List<ABody> Bodies { get => _bodies; internal set => _bodies = value; }
        /// <summary>
        /// The age of the system, in Gy
        /// </summary>
        public double SystemAge { get => _systemAge; internal set => _systemAge = value; }
        /// <summary>
        /// list of orbits 
        /// </summary>
        public List<Orbit> Orbits { get => _orbits; set => _orbits = value; }
        /// <summary>
        /// Number of stars
        /// </summary>
        public int StarCount { get => starCount;  }
        /// <summary>
        /// Number of planets
        /// </summary>
        public int PlanetCount { get => planetCount; }
        /// <summary>
        /// Gets the name of the system.
        /// </summary>
        public string Name => _name;
        /// <summary>
        /// Gets the UUID of the system.
        /// </summary>
        public Guid Uuid => _uuid;
    }
}