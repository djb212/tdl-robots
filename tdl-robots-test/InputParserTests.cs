using System;
using System.Collections.Generic;
using System.Text;
using tdl_robots.Data;
using tdl_robots.Model;

namespace tdl_robots_test
{
    internal class InputParserTests
    {
        InputParser inputParser;

        [SetUp]
        public void Setup()
        {
            // Arrange
            inputParser = new InputParser();
        }

        [Test]
        public void Given_ValidInput_ParseInput_Returns_CorrectResult()
        {
            // Act
            RobotsInput result = inputParser.ParseInput("input/validtestinput.txt");
            
            // Assert
            Assert.That(result.xBound, Is.EqualTo(5));
            Assert.That(result.yBound, Is.EqualTo(3));

            Assert.That(result.robots.Count, Is.EqualTo(3));
            Assert.That(result.robots[2].instructions, Is.EqualTo("LLFFFLFLFL"));
            Assert.That(result.robots[2].position.x, Is.EqualTo(0));
            Assert.That(result.robots[2].position.y, Is.EqualTo(3));
            Assert.That(result.robots[2].position.direction, Is.EqualTo(3));
        }

        [TestCase("input/doesnotexist.txt", typeof(FileNotFoundException))]
        [TestCase("input/boundstoobig.txt", typeof(ArgumentOutOfRangeException))]
        [TestCase("input/startoutofbounds.txt", typeof(ArgumentOutOfRangeException))]
        [TestCase("input/invalidstartdirection.txt", typeof(FormatException))]
        [TestCase("input/toomanycoordinates.txt", typeof(FormatException))]
        public void Given_InvalidInput_ParseInput_Throws_Exception(string file, Type exceptionType)
        {
            // Act & Assert
            Assert.Throws(exceptionType, () => inputParser.ParseInput(file));
        }
    }
}
