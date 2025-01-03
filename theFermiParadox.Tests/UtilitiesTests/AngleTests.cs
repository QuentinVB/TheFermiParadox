using FluentAssertions;
using NUnit.Framework;
using System;
using theFermiParadox.Core.Utilities;

namespace theFermiParadox.Tests.UtilitiesTests
{
    [TestFixture]
    public class AngleTests
    {
        [Test]
        public void Constructor_ShouldInitializeCorrectly()
        {
            // Arrange
            double angleValue = 90;
            AngleUnit unit = AngleUnit.degree;

            // Act
            var angle = new Angle(angleValue, unit);

            // Assert
            angle.AngleValue.Should().Be(angleValue);
            angle.Unit.Should().Be(unit);
        }

        [Test]
        public void AdditionOperator_ShouldAddAnglesOfSameUnit()
        {
            // Arrange
            var angle1 = new Angle(90, AngleUnit.degree);
            var angle2 = new Angle(45, AngleUnit.degree);

            // Act
            var result = angle1 + angle2;

            // Assert
            result.AngleValue.Should().Be(135);
            result.Unit.Should().Be(AngleUnit.degree);
        }

        [Test]
        public void AdditionOperator_ShouldThrowExceptionForDifferentUnits()
        {
            // Arrange
            var angle1 = new Angle(90, AngleUnit.degree);
            var angle2 = new Angle(Math.PI, AngleUnit.radian);

            // Act
            Action action = () => { var result = angle1 + angle2; };

            // Assert
            action.Should().Throw<ArgumentException>()
                  .WithMessage("Both angle must be on same AngleUnit");
        }

        [Test]
        public void DivisionOperator_ShouldThrowDivideByZeroException()
        {
            // Arrange
            var angle = new Angle(90, AngleUnit.degree);

            // Act
            Action action = () => { var result = angle / 0; };

            // Assert
            action.Should().Throw<DivideByZeroException>();
        }

        [Test]
        public void ToRadian_ShouldConvertDegreesToRadians()
        {
            // Arrange
            var angle = new Angle(180, AngleUnit.degree);

            // Act
            var result = angle.ToRadian();

            // Assert
            result.AngleValue.Should().BeApproximately(Math.PI, 1e-6);
            result.Unit.Should().Be(AngleUnit.radian);
        }

        [Test]
        public void ToDegree_ShouldConvertRadiansToDegrees()
        {
            // Arrange
            var angle = new Angle(Math.PI, AngleUnit.radian);

            // Act
            var result = angle.ToDegree();

            // Assert
            result.AngleValue.Should().BeApproximately(180, 1e-6);
            result.Unit.Should().Be(AngleUnit.degree);
        }

        [Test]
        public void ImplicitConversionToDouble_ShouldReturnAngleValue()
        {
            // Arrange
            var angle = new Angle(90, AngleUnit.degree);

            // Act
            double result = angle;

            // Assert
            result.Should().Be(90);
        }

        [Test]
        public void ImplicitConversionFromDouble_ShouldCreateAngleInRadians()
        {
            // Arrange
            double angleValue = Math.PI;

            // Act
            Angle result = angleValue;

            // Assert
            result.AngleValue.Should().Be(angleValue);
            result.Unit.Should().Be(AngleUnit.radian);
        }

        [Test]
        public void ToString_ShouldReturnAngleValueAsString()
        {
            // Arrange
            var angle = new Angle(90, AngleUnit.degree);

            // Act
            var result = angle.ToString();

            // Assert
            result.Should().Be("90");
        }
    }
}
