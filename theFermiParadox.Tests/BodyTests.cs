using NUnit.Framework;
using FluentAssertions;
using theFermiParadox.Core;

namespace Tests
{
    public class BodyTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void instances_empty_star()
        {
            //Arrange
            Star sut = new Star();
            //Act
            var readiness = sut.IsReady();
            //Assert
            readiness.Should().BeFalse();
            sut.Name.Should().Be("x");
        }
    }
}