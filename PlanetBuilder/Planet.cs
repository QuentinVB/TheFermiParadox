using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanetBuilder.Abstracts;
using PlanetBuilder.Helpers;
using System.Reflection;

namespace PlanetBuilder
{
    public class Planet : ABody<Planet>
    {
        private double _radius;
        private double _albedo;
        private TimeSpan _rotationPeriod;
        private PlanetaryComponentsContainer _planetaryComponents;


        /*
         * rotation speed
         * atmosphère ?
         */
        //list of all parameters of a planet
        public Planet():base("default",0,null)
        { }
        public Planet(double mass,string name,World world, double radius):base(name,mass, world)
        {
            _radius = radius;
            _planetaryComponents = new PlanetaryComponentsContainer();
        }
        public PlanetaryComponentsContainer PlanetaryComponents { get => _planetaryComponents; set => _planetaryComponents = value; }
        /// <summary>
        /// surface gravity based on g' acceleration, in m.s-2
        /// </summary>
        public double SurfaceGravity => (World.G * Mass) / (Radius * Radius); //m.s-2
        /// <summary>
        /// average radius of the planet, in m
        /// </summary>
        public double Radius { get => _radius; set => _radius = value; }
        /// <summary>
        /// rotation period of the planet
        /// </summary>
        public TimeSpan RotationPeriod { get => _rotationPeriod; set => _rotationPeriod = value; }
        /// <summary>
        /// Circumference based on radii, in m
        /// </summary>
        public double Circumference { get => _radius * Math.PI * 2; }
        /// <summary>
        /// ground surface based on radii, in m2
        /// </summary>
        public double Surface { get => _radius* _radius * Math.PI * 4; }
        /// <summary>
        /// Volume of the planet based on radii, in m3
        /// </summary>
        public double Volume { get => (_radius* _radius * Math.PI * 4)/3; }
        /// <summary>
        /// Density of the planet. in kg/m3
        /// </summary>
        public double Density { get => Mass / Volume; }
        /// <summary>
        /// Speed to escape the gravity well, in m.s-1 
        /// </summary>
        public double EscapeVelocity { get => Math.Sqrt((2 * World.G * Mass) / Radius); }

        public double Albedo { get => _albedo; set => _albedo = value; }


        public override string ToString()
        {
            string rslt = "Planet Data : \n";
            foreach (PropertyInfo p in this.GetType().GetProperties().ToList())
            {
                rslt += "  " + p.Name + " : " + this.GetType().GetProperty(p.Name).GetValue(this, null).ToString()+"\n";
            }
            return rslt;
        }


    }
}
/*
--star
--asteroid
--planet
--belt
--gazCloud
--artifical

*/
