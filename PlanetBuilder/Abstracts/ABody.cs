using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanetBuilder.Interfaces;

namespace PlanetBuilder.Abstracts
{
    public abstract class ABody<T> : AOrbitalObject<T>, IBody
    {
        private double _mass;
        public double Mass { get => _mass; set => _mass = value; }

        protected ABody(string name, double mass, World world) : base(name, world)
        {
            _mass = mass;
        }

       
    }
}
