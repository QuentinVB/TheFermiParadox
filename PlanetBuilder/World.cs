using PlanetBuilder.Helpers;
using PlanetBuilder.Interfaces;

namespace PlanetBuilder
{
    public class World
    {
        public const double G = 6.67234e-11; //N⋅m2⋅kg-2
        public static double GravitationnalForce(IBody a, IBody b) => G * (a.Mass * b.Mass) / Vector3.Distance(a.Position, b.Position);

    }
   
    /*
     * orbital position to polar position
     * orbital position to  cartesian position
     * keplers law
     */
}