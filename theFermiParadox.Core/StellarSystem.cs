using System.Collections.Generic;
using theFermiParadox.Core.Abstracts;
using System.Linq;
using Helpers;

namespace theFermiParadox.Core
{
    public class StellarSystem
    {
        List<ABody> _bodies;
        ABody _objectRoot;
        double _systemAge;

        int starCount = 0;
        int planetCount = 0;

        public StellarSystem()
        {
            _bodies = new List<ABody>();
        }
        
        public void Add(ABody body)
        {
            _bodies.Add(body);

            if (body is Star) starCount ++;
            if (body is Planet) starCount ++;
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


        public ABody PhysicalObjectRoot { get => _objectRoot; private set => _objectRoot = value; }
        public List<ABody> Bodies { get => _bodies; internal set => _bodies = value; }
        public double SystemAge { get => _systemAge; internal set => _systemAge = value; }

        //list of stellar object associated with orbital parameters
        //list of orbits around each stellar objects
        //position from barycenter calculator
    }
}