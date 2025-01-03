using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using theFermiParadox.Core;
using theFermiParadox.Core.Utilities;
using theFermiParadox.Core.Interfaces;
using System.Numerics;

namespace theFermiParadox.Tests.UtilitiesTests
{
    [TestFixture]
    public class OrbitTests
    {
        private class MockOrbitable : IOrbitable
        {
            public double Mass { get; set; }
            public bool IsVirtual { get; set; }
            public List<Orbit> ChildOrbits { get; set; } = new List<Orbit>();
            public Orbit ParentOrbit { get; set; }
            public Vector3 Position { get; set; }

            public string Name => "Mockito";

            public double Radius => 1;

            public void Accept(Visitor v)
            {
                throw new NotImplementedException();
            }

            public INode Accept(MutationVisitor v)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void Constructor_ShouldInitializeCorrectly()
        {
            // Arrange
            var bodyA = new MockOrbitable { Mass = 5.972e24 }; // Earth mass
            var bodyB = new MockOrbitable { Mass = 7.348e22 }; // Moon mass
            var epoch = DateTime.UtcNow;
            double eccentricity = 0.0549;
            double meanDistance = 3.844e8;

            // Act
            var orbit = new Orbit(bodyA, bodyB, epoch, eccentricity, meanDistance);

            // Assert
            orbit.Apoapsis.Should().BeApproximately(4.067e8, 1e5);
            orbit.Periapsis.Should().BeApproximately(3.621e8, 1e5);
            orbit.Eccentricity.Should().BeApproximately(eccentricity, 1e-4);
            orbit.Epoch.Should().Be(epoch);
        }

        /*
        [TestCase(0.0, 1.0, ExpectedResult = 1.0)]
        [TestCase(0.5, 0.5, ExpectedResult = 0.0)]
        [TestCase(1.0, 0.0, ExpectedResult = -1.0)]
        public double ComputeEccentricAnomaly_ShouldReturnCorrectValue(double meanAnomaly, double eccentricity)
        {
            // Arrange
            var orbit = new Orbit();
            var method = typeof(Orbit).GetMethod("ComputeEccentricAnomaly", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            double result = (double)method.Invoke(orbit, new object[] { eccentricity, meanAnomaly });

            // Assert
            return result;
        }
        */
        [Test]
        public void UpdateTime_ShouldUpdatePropertiesCorrectly()
        {
            // Arrange
            var bodyA = new MockOrbitable { Mass = 5.972e24 };
            var bodyB = new MockOrbitable { Mass = 7.348e22 };
            var orbit = new Orbit(bodyA, bodyB, DateTime.UtcNow, 0.0167, 1.496e11);
            var timeOffset = TimeSpan.FromSeconds(86400); // 1 day

            // Act
            orbit.UpdateTime(timeOffset);

            // Assert
            orbit.ElapsedTime.Should().Be((ulong)timeOffset.TotalSeconds);
            orbit.MeanAnomaly.AngleValue.Should().BeGreaterThan(0);
        }

        [Test]
        public void OffsetPeriod_ShouldThrowException_WhenFractionIsInvalid()
        {
            // Arrange
            var bodyA = new MockOrbitable { Mass = 1e30 };
            var bodyB = new MockOrbitable { Mass = 1e24 };
            var orbit = new Orbit(bodyA, bodyB, DateTime.UtcNow, 0.1, 1e11);

            // Act
            Action action = () => orbit.OffsetPeriod(-0.1);

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>()
                  .WithMessage("*fraction should be between 0 and 1*");
        }

        [Test]
        public void Clone_ShouldReturnIdenticalObject()
        {
            // Arrange
            var bodyA = new MockOrbitable { Mass = 1e30 };
            var bodyB = new MockOrbitable { Mass = 1e24 };
            var orbit = new Orbit(bodyA, bodyB, DateTime.UtcNow, 0.1, 1e11);

            // Act
            var clonedOrbit = (Orbit)orbit.Clone();

            // Assert
            clonedOrbit.Should().NotBeSameAs(orbit);
            clonedOrbit.Apoapsis.Should().BeApproximately(orbit.Apoapsis, 1e-5);
        }
    }
}
