using NUnit.Framework;
using FluentAssertions;
using theFermiParadox.Core;
using theFermiParadox.DAL;
using System.Collections.Generic;

namespace Tests
{
    public class CSVLoaderTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("starGeneration.csv",9)]
        public void Loader_tests_starGeneration(string filename,int itemCount)
        {
            //Arrange

            //Act
            List<StarGeneration> sut = Loader<StarGeneration>.LoadTable(filename);

            //Assert
            sut.Count.Should().Be(itemCount);
            //basicStarSource.ToArray().GetValue(0)
        }
        [Test]
        [TestCase("basicStar.csv", 150)]
        public void Loader_tests_basicStar(string filename, int itemCount)
        {
            //Arrange

            //Act
            List<BasicStar> sut = Loader<BasicStar>.LoadTable(filename);

            //Assert
            sut.Count.Should().Be(itemCount);
            //basicStarSource.ToArray().GetValue(0)
        }
        [Test]
        [TestCase("systemAge.csv", 120)]
        public void Loader_tests_starAge(string filename, int itemCount)
        {
            //Arrange

            //Act
            List<StarAge> sut = Loader<StarAge>.LoadTable(filename);

            //Assert
            sut.Count.Should().Be(itemCount);
            //basicStarSource.ToArray().GetValue(0)
        }
        [Test]
        [TestCase("browndwarf.csv", 10)]
        [TestCase("whitedwarf.csv", 10)]
        public void Loader_tests_dwarves(string filename, int itemCount)
        {
            //Arrange

            //Act
            List<DwarfStar> sut = Loader<DwarfStar>.LoadTable(filename);

            //Assert
            sut.Count.Should().Be(itemCount);
            //basicStarSource.ToArray().GetValue(0)
        }
    }
}