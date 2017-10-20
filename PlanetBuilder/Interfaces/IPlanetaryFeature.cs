using System;
using System.Collections.Generic;

namespace PlanetBuilder.Interfaces
{
    public interface IPlanetaryFeature
    {
        string Name { get; set; }
        List<String> Incompatibility { get; }
    }
}