using System.Collections.Generic;

namespace theFermiParadox.Core.Abstracts
{
    public interface IOrbitable
    {
        bool IsVirtual { get; }
        double Radius { get; }
        double Mass { get; }
        Orbit ParentOrbit { get; set; }
        List<Orbit> ChildOrbit { get; set; }
    }
}