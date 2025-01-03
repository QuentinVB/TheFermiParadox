using FluentAssertions;
using NUnit.Framework;
using System;
using theFermiParadox.Core;
using theFermiParadox.Core.Utilities;

namespace theFermiParadox.Tests.UtilitiesTests
{
    [TestFixture]
    public class PhysicTests
    {
        [Test]
        public void Constants_ShouldHaveCorrectValues()
        {
            Physic.GravitationalConstant.Should().BeApproximately(6.673e-11, 1e-15);
            Physic.SolarMass.Should().BeApproximately(1.9884e30, 1e26);
            Physic.SolarRadius.Should().BeApproximately(6.957e8, 1e4);
            Physic.LightSpeed.Should().Be(299792458);
        }

        [Test]
        public void GravitationnalForce_ShouldReturnCorrectForce()
        {
            // Arrange
            double massA = 5.972e24; // Earth mass in kg
            double massB = 7.348e22; // Moon mass in kg
            double distance = 3.844e8; // Distance in meters

            // Act
            double result = Physic.GravitationnalForce(massA, massB, distance);

            // Assert
            result.Should().BeApproximately(1.982e20, 1e15);
        }

        [Test]
        public void GravitationnalForce_ShouldThrowException_WhenDistanceIsZero()
        {
            // Arrange
            double massA = 1e3;
            double massB = 1e3;
            double distance = 0;

            // Act
            Action action = () => Physic.GravitationnalForce(massA, massB, distance);

            // Assert
            action.Should().Throw<DivideByZeroException>();
        }

        [Test]
        public void ColorTemperatureToRGB_ShouldReturnCorrectRGB()
        {
            // Arrange
            int kelvin = 6500; // Standard daylight

            // Act
            var result = Physic.ColorTemperatureToRGB(kelvin);

            // Assert
            result.R.Should().BeInRange(0, 255);
            result.G.Should().BeInRange(0, 255);
            result.B.Should().BeInRange(0, 255);
        }

        [Test]
        public void ClampInt_ShouldClampValueWithinRange()
        {
            // Arrange
            int value = 10;

            // Act
            int clampedValue = Physic.Clamp(value, 5, 15);

            // Assert
            clampedValue.Should().Be(10);
        }

        [Test]
        public void ClampFloat_ShouldClampValueWithinRange()
        {
            // Arrange
            float value = 20.5f;

            // Act
            float clampedValue = Physic.Clamp(value, 10.0f, 20.0f);

            // Assert
            clampedValue.Should().Be(20.0f);
        }

        [Test]
        public void LatinNumber_ShouldReturnCorrectRomanNumerals()
        {
            // Arrange
            int number = 4;

            // Act
            string result = Physic.LatinNumber(number);

            // Assert
            result.Should().Be("IV");
        }

        [Test]
        public void StarLetter_ShouldReturnCorrectLetter_WhenIndexIsValid()
        {
            // Arrange
            int index = 0;

            // Act
            string result = Physic.StarLetter(index);

            // Assert
            result.Should().Be("A");
        }

        [Test]
        public void StarLetter_ShouldThrowException_WhenIndexIsInvalid()
        {
            // Arrange
            int index = -1;

            // Act
            Action action = () => Physic.StarLetter(index);

            // Assert
            action.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        public void SphereVolume_ShouldReturnCorrectVolume()
        {
            // Arrange
            double radius = 1;

            // Act
            double volume = Physic.SphereVolume(radius);

            // Assert
            volume.Should().BeApproximately(4.18879, 1e-5);
        }
    }
}
