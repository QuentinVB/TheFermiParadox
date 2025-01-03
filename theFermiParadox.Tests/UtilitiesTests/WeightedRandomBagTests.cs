using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using theFermiParadox.Core.Utilities;

namespace theFermiParadox.Tests.UtilitiesTests
{
    [TestFixture]
    public class WeightedRandomBagTests
    {
        [Test]
        public void AddEntry_ShouldAddEntriesCorrectly()
        {
            // Arrange
            var bag = new WeightedRandomBag<string>();

            // Act
            bag.AddEntry("A", 1.0);
            bag.AddEntry("B", 2.0);

            // Assert
            bag.GetRandom().Should().NotBeNull(); // At least one entry should be available.
        }

        [Test]
        public void GetRandom_ShouldReturnDefault_WhenNoEntriesExist()
        {
            // Arrange
            var bag = new WeightedRandomBag<int>();

            // Act
            var result = bag.GetRandom();

            // Assert
            result.Should().Be(default(int));
        }

        [Test]
        public void GetRandom_ShouldAlwaysReturnSingleEntry_WhenOnlyOneEntryExists()
        {
            // Arrange
            var bag = new WeightedRandomBag<string>();
            bag.AddEntry("OnlyEntry", 1.0);

            // Act
            var result = bag.GetRandom();

            // Assert
            result.Should().Be("OnlyEntry");
        }

        [Test]
        public void GetRandom_ShouldReflectWeightsCorrectly()
        {
            // Arrange
            var bag = new WeightedRandomBag<string>();
            bag.AddEntry("Low", 1.0);
            bag.AddEntry("High", 9.0);

            var results = new Dictionary<string, int> { { "Low", 0 }, { "High", 0 } };

            // Act
            for (int i = 0; i < 1000; i++)
            {
                var result = bag.GetRandom();
                results[result]++;
            }

            // Assert
            results["High"].Should().BeGreaterThan(results["Low"]);
        }
    }
}
