using Helpers;
using System.Collections.Generic;

namespace theFermiParadox.Core.Abstracts
{
    public interface IOrbitable : INode
    {
        string Name { get; }
        bool IsVirtual { get; }
        double Radius { get; }
        double Mass { get; }
        Vector3 Position { get; set; }
        Orbit ParentOrbit { get; set; }
        List<Orbit> ChildOrbits { get; set; }
    }
}