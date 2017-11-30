using PlanetBuilder.Abstracts;
using System.Linq;
using System.Reflection;

namespace PlanetBuilder.UI
{
    public class Atmosphere : PlanetaryFeature
    {
        public Atmosphere():base("Atmosphere","the Atmospheric composition")
        {

        }
        public override string ToString()
        {
            string rslt = "Atmospheric Data : \n";
            foreach (PropertyInfo p in this.GetType().GetProperties().ToList())
            {
                rslt += "  " + p.Name + " : " + this.GetType().GetProperty(p.Name).GetValue(this, null) + "\n";
            }
            return rslt;
        }
        public double Oxygen { get => Oxygen; set => Oxygen = value; }
    }
}