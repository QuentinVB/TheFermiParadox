using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using theFermiParadox.Core.Abstracts;
using theFermiParadox.Core.Utilities;

namespace theFermiParadox.Core
{
    public class Planet : APhysicalObject
    {
        private double _albedo;
        private PlanetaryComponentsContainer _planetaryComponents;

        public Planet()
           : base(null)
        {
        }
        public Planet(StellarSystem stellarSystem)
           : base(stellarSystem)
        {
            _planetaryComponents = new PlanetaryComponentsContainer();
        }

        public override string Denomination
        {
            get
            {
                return "A planet";
            }
        }   
       
        public PlanetaryComponentsContainer PlanetaryComponents { get => _planetaryComponents; set => _planetaryComponents = value; }
        
        /// <summary>
        /// Circumference based on radii, in m
        /// </summary>
        public double Circumference { get => RadiusInM * Math.PI * 2; }
        /// <summary>
        /// ground surface based on radii, in m2
        /// </summary>
        public double Surface { get => RadiusInM * RadiusInM * Math.PI * 4; }
        /// <summary>
        /// Volume of the planet based on radii, in m3
        /// </summary>
        public override double Volume { get => Physic.SphereVolume(RadiusInM); }
        /// <summary>
        /// Density of the planet. in kg/m3
        /// </summary>
        

        public double Albedo { get => _albedo; set => _albedo = value; }


    }
}
