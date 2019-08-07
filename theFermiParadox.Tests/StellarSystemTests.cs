using NUnit.Framework;
using FluentAssertions;
using theFermiParadox.Core;

namespace Tests
{
    public class StellarSystemTests
    {
        [SetUp]
        public void Setup()
        {
        }

        /*
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public void Intersect_star_orbits(int starCount)
        {
            //Arrange
            SystemFactory factory = new SystemFactory();
            //act
            StellarSystem sut = factory.GetStellarSystem(starCount);

            sut.Orbits.Sort((x, y) =>
            {
                return y.SemiMajorAxis.CompareTo(x.SemiMajorAxis);
            });

            //assert
            Orbit actual = ;
            Orbit previous;
            foreach (Orbit orbit in sut.Orbits)
            {

            }
        }
        */
    }
}