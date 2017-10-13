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
        private PlanetaryCategory _planetaryCategory;


        /*
         * périmètre
         * superficie
         * volume
         * masse volumique
         * gravité en surface
         * vitesse de libération
         * rotation speed
         * atmosphère ?
         */
        //list of all parameters of a planet
        public Planet():base("default",0,null)
        { }
        public Planet(double mass,string name,World world, double radius):base(name,mass, world)
        {
            _radius = radius;
        }

        public double SurfaceGravity => (World.G * Mass) / (Radius * Radius); //m.s-2

        public double Radius { get => _radius; set => _radius = value; }
        public double Albedo { get => _albedo; set => _albedo = value; }
        public TimeSpan RotationPeriod { get => _rotationPeriod; set => _rotationPeriod = value; }
        public PlanetaryCategory PlanetaryCategory { get => _planetaryCategory; set => _planetaryCategory = value; }

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
