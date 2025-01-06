using System.Collections.Generic;
using System.Numerics;
using theFermiParadox.Core.Utilities;

namespace theFermiParadox.Core.Interfaces
{
    public interface IOrbitable : IBody
    {
        bool IsVirtual { get; }
        double Radius { get; }
        double Mass { get; }
        Vector3 Position { get; set; }
        Orbit ParentOrbit { get; set; }
        List<Orbit> ChildOrbits { get; set; }
    }
}