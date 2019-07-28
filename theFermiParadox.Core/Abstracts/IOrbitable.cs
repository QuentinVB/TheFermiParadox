using System.Collections.Generic;

namespace theFermiParadox.Core.Abstracts
{
    public interface IOrbitable
    {
        bool IsVirtual { get; set; }
        double Mass { get; set; }
        Orbit ParentOrbit { get; set; }
        List<Orbit> ChildOrbit { get; set; }
    }
}