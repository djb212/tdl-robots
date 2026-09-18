using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using tdl_robots.Model;
using tdl_robots.Services;

namespace tdl_robots_test
{
    internal class ProcessRobotsTests
    {
        private RobotsInput input;

        [SetUp]
        public void Setup()
        {
            // Arrange
            input = new RobotsInput
            {
                xBound = 5,
                yBound = 3,
                robots = new List<Robot>()
            };

        }

        /// <summary>
        /// Tests the ProcessRobots class with valid input and checks if the output matches the expected result.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="direction"></param>
        /// <param name="instructions"></param>
        /// <param name="expectedOutput"></param>
        [TestCase(1, 1, 1, "RFRFRFRF", "1 1 E")]
        [TestCase(3, 2, 0, "FRRFLLFFRRFLL", "3 3 N LOST")]
        [TestCase(0, 3, 3, "LLFFFLFLFL", "3 3 N LOST")]
        public void Given_ValidInput_ProcessRobots_Returns_CorrectResult(int x, int y, int direction, string instructions, string expectedOutput)
        {
            // Arrange
            input.robots.Add(new Robot(x, y, direction, instructions));

            ProcessRobots processRobots = new ProcessRobots(input);

            // Act
            string[] result = processRobots.Run();

            // Assert
            Assert.That(result, Has.Length.EqualTo(1));
            Assert.That(result[0], Is.EqualTo(expectedOutput));
        }

        /// <summary>
        /// Tests the ProcessRobots class with a lost robot and checks that the next robot is not lost at the same square.
        /// </summary>
        [Test]
        public void Given_LostRobot_ProcessRobots_Returns_RobotNotLostAtSameSquare()
        {
            // Arrange
            input.robots.Add(new Robot(3, 2, 0, "FRRFLLFFRRFLL")); // This robot will be lost
            input.robots.Add(new Robot(0, 3, 3, "LLFFFLFLFL")); // This robot should not be lost

            ProcessRobots processRobots = new ProcessRobots(input);

            // Act
            string[] result = processRobots.Run();

            // Assert
            Assert.That(result, Has.Length.EqualTo(2));
            Assert.That(result[0], Is.EqualTo("3 3 N LOST"));
            Assert.That(result[1], Is.EqualTo("2 3 S"));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid instruction character is provided for a robot.
        /// </summary>
        [Test]
        public void Given_InvalidInstruction_ProcessRobots_Throws_Exception()
        {
            // Arrange
            input.robots.Add(new Robot(3, 2, 0, "TURNIP"));

            ProcessRobots processRobots = new ProcessRobots(input);

            // Act & Assert
            Exception ex = Assert.Throws<FormatException>(() => processRobots.Run());
            Assert.That(ex.Message, Is.EqualTo("Invalid instruction character"));
        }

    }
}
