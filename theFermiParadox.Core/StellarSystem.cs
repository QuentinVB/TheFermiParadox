using System.Collections.Generic;
using theFermiParadox.Core.Abstracts;
using System.Linq;
using Helpers;

namespace theFermiParadox.Core
{
    public class StellarSystem
    {
        List<ABody> _bodies;
        APhysicalObject _PhysicalObjectRoot;
        List<IOrbit> _orbits;

        public StellarSystem()
        {
            _bodies = new List<ABody>();
        }
        
        public void Add(ABody body)
        {
            _bodies.Add(body);
            
            /*
            if(body.GetType().Equals(typeof(AStellarObject))){ _stellarObjects.Add(body as AStellarObject); }
            if(body.GetType().Equals(typeof(ASystemObject))){ _systemObjects.Add(body as ASystemObject); }
            _orbits.Add(body.GetOrbit);
            */
        }

        public Vector3 Barycenter { get; }

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

        public List<IOrbit> Orbits { get => _orbits; set => _orbits = value; }

        public APhysicalObject PhysicalObjectRoot { get => _PhysicalObjectRoot; private set => _PhysicalObjectRoot = value; }

        //list of stellar object associated with orbital parameters
        //list of orbits around each stellar objects
        //position from barycenter calculator
    }
}