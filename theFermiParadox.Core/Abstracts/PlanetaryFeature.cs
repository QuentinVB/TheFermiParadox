using System.Collections.Generic;
using theFermiParadox.Core.Interfaces;

namespace theFermiParadox.Abstracts
{
    public abstract class APlanetaryFeature : IPlanetaryFeature
    {
        readonly string _name;
        readonly string _description;
        List<string> _incompatibility;

        public string Name
        {
            get { return _name; }
        }

        public APlanetaryFeature(string name, string description)
        {
            _name = name;
            _description = description;
        }
        public List<string> Incompatibility
        {
            get => _incompatibility;
            internal set
            {
                _incompatibility = value;
            }
        }
        public string Description { get => _description; }
    }
}
