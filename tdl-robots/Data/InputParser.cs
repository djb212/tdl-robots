using tdl_robots.Model;

namespace tdl_robots.Data
{
    /// <summary>
    /// Reads and parses the input file, validating the data and returning a RobotsInput object.
    /// </summary>
    internal class InputParser
    {
        /// <summary>
        /// X bound of the grid
        /// </summary>
        int xBound;

        /// <summary>
        /// Y bound of the grid
        /// </summary>
        int yBound;

        /// <summary>
        /// Parses the input file and returns a RobotsInput object containing the grid bounds and a list of robots with their starting positions and instructions.
        /// </summary>
        /// <param name="inputFile">URI of the input file to be read</param>
        /// <returns>Formatted RobotsInput object containing the input data</returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="FormatException"></exception>
        internal RobotsInput ParseInput(string inputFile)
        {
            // Read the file
            string[] lines = Array.Empty<string>();

            try
            {
                lines = File.ReadAllLines(inputFile);
            }
            catch (Exception ex)
            {
                throw new FileNotFoundException("Error reading input file: " + ex.Message);
            }

            // Make sure we've actually got some input
            if (lines.Length == 0)
            {
                throw new FormatException("Input file is empty");
            }

            GetBounds(lines[0]);

            RobotsInput robotsInput = new RobotsInput
            {
                xBound = xBound,
                yBound = yBound
            };

            // Now parse and validate the robots

            for (int i = 0; i < lines.Length / 2; i++)
            {
                robotsInput.robots.Add(ParseRobot(lines[2 * i + 1], lines[2 * i + 2]));
            }

            return robotsInput;
        }

        /// <summary>
        /// Parses the bounds from the first line of the input file and validates them.
        /// </summary>
        /// <param name="boundsString">First line of the input file containing the bounds</param>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void GetBounds(string boundsString)
        {
            string[] bounds = boundsString.Trim().Split();
            if (bounds.Length != 2)
            {
                throw new FormatException("Invalid bounds format");
            }
            
            if (!int.TryParse(bounds[0], out xBound) || !int.TryParse(bounds[1], out yBound))
            {
                throw new FormatException("Bounds must be integers");
            }

            if (xBound < 0 || xBound > 50 || yBound < 0 || yBound > 50)
            {
                throw new ArgumentOutOfRangeException("Bounds must be between 0 and 50");
            }
        }

        /// <summary>
        /// Parses a robot's starting position and instructions from the input file and validates them.
        /// </summary>
        /// <param name="positionString">First line of the robot's description, containing its starting position and direction</param>
        /// <param name="instructionString">Second line of the robot's description, containing its movement instructions</param>
        /// <returns>Robot object</returns>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private Robot ParseRobot(string positionString, string instructionString)
        {
            // Parse and validate starting position
            string[] coords = positionString.Trim().Split();

            if (coords.Length != 3)
            {
                throw new FormatException("Invalid robot position format");
            }

            if (!int.TryParse(coords[0], out int xPosition) || !int.TryParse(coords[1], out int yPosition))
            {
                throw new FormatException("Robot coordinates must be integers");
            }

            if (xPosition < 0 || xPosition > xBound || yPosition < 0 || yPosition > yBound)
            {
                throw new ArgumentOutOfRangeException("Robot coordinates out of bounds");
            }

            int direction = coords[2] switch
            {
                "N" => 0,
                "E" => 1,
                "S" => 2,
                "W" => 3,
                _ => throw new FormatException("Invalid robot direction")
            };

            // Validate instructions
            instructionString = instructionString.Trim();
            if (instructionString.Length >= 100)
            {
                throw new FormatException("Instructions should be 99 or fewer characters");
            }

            return new Robot(xPosition, yPosition, direction, instructionString);
        }
    }
}
