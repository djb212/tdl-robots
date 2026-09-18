using tdl_robots.Model;

namespace tdl_robots.Services
{
    internal class ProcessRobots
    {
        /// <summary>
        /// Parsed input object containing the grid bounds and list of robots with their starting positions and instructions.
        /// </summary>
        private RobotsInput input;

        /// <summary>
        /// Set of positions where robots have been lost, to prevent future robots from being lost at the same position.
        /// As we're only interested in checking if a robot has been lost at a position, a hashset is more efficient than a list.
        /// </summary>
        private HashSet<string> lostPositions;

        /// <summary>
        /// Constructor for class
        /// </summary>
        /// <param name="robotsInput">RobotsInput object</param>
        internal ProcessRobots(RobotsInput robotsInput)
        {
            input = robotsInput;
            lostPositions = new HashSet<string>();
        }

        /// <summary>
        /// Processes the robots in the input, following their instructions and returning their final positions and lost status.
        /// </summary>
        /// <returns>Array of strings with the final position of each robot</returns>
        internal string[] Run()
        {
            string[] output = new string[input.robots.Count];
            for (int i = 0; i < input.robots.Count; i++)
            {
                output[i] = ProcessRobot(input.robots[i]);
            }
            return output;
        }

        /// <summary>
        /// Processes a single robot's instructions, updating its position and checking for lost status.
        /// </summary>
        /// <param name="robot">Robot object with location and instructions</param>
        /// <returns>String representing final position and, if applicable, lost status</returns>
        /// <exception cref="FormatException"></exception>
        internal string ProcessRobot(Robot robot)
        {
            Position position = robot.position;
            bool isLost = false;
            // Iterate over the instructions
            foreach (char c in robot.instructions)
            {
                switch (c)
                {
                    case 'L':
                        // Left turn
                        position.direction = (position.direction + 3) % 4;
                        break;
                    case 'R':
                        // Right turn
                        position.direction = (position.direction + 1) % 4;
                        break;
                    case 'F':
                        // Move forwards
                        int x = position.x;
                        int y = position.y;
                        switch (position.direction)
                        {
                            case 0: // N
                                y += 1;
                                break;
                            case 1: // E
                                x += 1;
                                break;
                            case 2: // S
                                y -= 1;
                                break;
                            case 3: // W
                                x -= 1;
                                break;
                        }

                        // Check if out of bounds
                        if (x < 0 || x > input.xBound || y < 0 || y > input.yBound)
                        {
                            // Check if we've already lost a robot here
                            if (lostPositions.Contains(position.x + "," + position.y))
                            {
                                // Continue to next instruction
                                continue;
                            }
                            else
                            {
                                // Robot is lost
                                isLost = true;
                                lostPositions.Add(position.x + "," + position.y);
                            }
                        }
                        else
                        {
                            // Still in bounds - update position
                            position.x = x;
                            position.y = y;
                        }
                        break;

                    // Add any new instruction types here

                    default:
                        throw new FormatException("Invalid instruction character");

                }

                // If lost, stop following instructions
                if (isLost)
                {
                    break;
                }
            }

            // Convert the direction back to a string
            string directionStr = position.direction switch
            {
                0 => "N",
                1 => "E",
                2 => "S",
                3 => "W",
                _ => throw new FormatException("Invalid direction state")
            };

            // Return the formatted output
            return $"{position.x} {position.y} {directionStr}" + (isLost ? " LOST" : "");

        }
    }
}
