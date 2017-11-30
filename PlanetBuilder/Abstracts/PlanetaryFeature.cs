using System;
using System.Collections.Generic;
using PlanetBuilder.Interfaces;
using System.Xml.Serialization;

namespace PlanetBuilder.Abstracts
{
    public abstract class PlanetaryFeature : IPlanetaryFeature
    {
        readonly string _name;
        readonly string _description;
        List<string> _incompatibility;

        public string Name { get { return _name; }
        }

        public PlanetaryFeature(string name,string description)
        {
            _name = name;
            _description = description;
        }
        public List<string> Incompatibility { get => _incompatibility;
            internal set
            {
                _incompatibility = value;
            }
        }
        public string Description { get=> Description; }
    }
}