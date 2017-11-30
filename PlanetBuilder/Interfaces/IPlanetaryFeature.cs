using System;
using System.Collections.Generic;

namespace PlanetBuilder.Interfaces
{
    public interface IPlanetaryFeature
    {
        string Name { get;  }
        string Description { get;  }
        List<String> Incompatibility { get; }
    }
}