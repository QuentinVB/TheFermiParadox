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
        //private ChemicalComposition _chemicalComposition;
        private PlanetaryModifer _planetaryModifer;


        /*
         * rotation speed
         * atmosphère ?
         */
        //list of all parameters of a planet
        public Planet():base("default",0,null)
        { }
        public Planet(double mass,string name,World world, double radius,PlanetaryModifer planetaryModifer):base(name,mass, world)
        {
            _radius = radius;
        }

        public double SurfaceGravity => (World.G * Mass) / (Radius * Radius); //m.s-2

        public double Radius { get => _radius; set => _radius = value; }
        public double Albedo { get => _albedo; set => _albedo = value; }
        public TimeSpan RotationPeriod { get => _rotationPeriod; set => _rotationPeriod = value; }
        public PlanetaryModifer PlanetaryModifer { get => _planetaryModifer; set => _planetaryModifer = value; }
        public double Circumference { get => _radius * Math.PI * 2; }
        public double Surface { get => _radius* _radius * Math.PI * 4; }
        public double Volume { get => (_radius* _radius * Math.PI * 4)/3; }
        /// <summary>
        /// Speed to escape the gravity well (in m.s-1)
        /// </summary>
        public double EscapeVelocity { get => Math.Sqrt((2 * World.G * Mass) / Radius); }


        public override string ToString()
        {
            string rslt = "Space Object Data : \n";
            foreach (PropertyInfo p in this.GetType().GetProperties().ToList())
            {
                rslt += "  " + p.Name + " : " + this.GetType().GetProperty(p.Name).GetValue(this, null)+"\n";
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
