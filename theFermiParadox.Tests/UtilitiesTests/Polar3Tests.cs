using FluentAssertions;
using NUnit.Framework;
using System;
using System.Numerics;
using theFermiParadox.Core.Utilities;

namespace theFermiParadox.Tests.UtilitiesTests
{
    [TestFixture]
    public class Polar3Tests
    {
        [Test]
        public void Constructor_ShouldInitializeCorrectly()
        {
            // Arrange
            double r = 5.0;
            var omega = new Angle(45, AngleUnit.degree);
            var phi = new Angle(30, AngleUnit.degree);

            // Act
            var polar = new Polar3(r, omega, phi);

            // Assert
            polar.R.Should().Be(r);
            polar.O.Should().Be(omega);
            polar.P.Should().Be(phi);
        }

        [Test]
        public void Constructor_ShouldThrowException_WhenRadiusIsNegative()
        {
            // Arrange
            double r = -5.0;
            var omega = new Angle(45, AngleUnit.degree);
            var phi = new Angle(30, AngleUnit.degree);

            // Act
            Action action = () => new Polar3(r, omega, phi);

            // Assert
            action.Should().Throw<ArgumentException>()
                  .WithMessage("polar Radius cant be negative");
        }

        [Test]
        public void AdditionOperator_ShouldAddPolarAndVector3()
        {
            // Arrange
            var polar = new Polar3(5.0, new Angle(0, AngleUnit.degree), new Angle(90, AngleUnit.degree));
            var vector = new Vector3(1.0f, 1.0f, 0.0f);

            // Act
            var result = polar + vector;

            // Assert
            result.ToCartesian().X.Should().BeApproximately(1.0f, 1e-6f);
            result.ToCartesian().Y.Should().BeApproximately(6.0f, 1e-6f);
            result.ToCartesian().Z.Should().BeApproximately(0.0f, 1e-6f);
        }

        [Test]
        public void Distance_ShouldReturnCorrectDistanceBetweenTwoPolarCoordinates()
        {
            // Arrange
            var polar1 = new Polar3(5.0, new Angle(0, AngleUnit.degree), new Angle(90, AngleUnit.degree));
            var polar2 = new Polar3(5.0, new Angle(90, AngleUnit.degree), new Angle(90, AngleUnit.degree));

            // Act
            var distance = Polar3.Distance(polar1, polar2);

            // Assert
            distance.Should().BeApproximately(7.071, 1e-3);
        }

        [Test]
        public void ToCartesian_ShouldConvertPolarToCartesian()
        {
            // Arrange
            var polar = new Polar3(5.0, new Angle(0, AngleUnit.degree), new Angle(90, AngleUnit.degree));

            // Act
            var cartesian = polar.ToCartesian();

            // Assert
            cartesian.X.Should().BeApproximately(5.0f, 1e-6f);
            cartesian.Y.Should().BeApproximately(0.0f, 1e-6f);
            cartesian.Z.Should().BeApproximately(0.0f, 1e-6f);
        }

        [Test]
        public void ToString_ShouldReturnCorrectFormat()
        {
            // Arrange
            var polar = new Polar3(5.0, new Angle(45, AngleUnit.degree), new Angle(30, AngleUnit.degree));

            // Act
            var result = polar.ToString();

            // Assert
            result.Should().Be("5:45:30");
        }
    }
}
