using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theFermiParadox.Core.Interfaces;

namespace theFermiParadox.Core.Abstracts
{
    public abstract class PlanetaryFeature : IPlanetaryFeature
    {
        readonly string _name;
        readonly string _description;
        List<string> _incompatibility;

        public string Name
        {
            get { return _name; }
        }

        public PlanetaryFeature(string name, string description)
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
