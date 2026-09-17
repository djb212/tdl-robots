using tdl_robots.Model;

namespace tdl_robots.Services
{
    internal class ProcessRobots
    {
        private readonly int xBound;
        private readonly int yBound;
        private readonly List<Robot> robots;
        private HashSet<int> lostPositions;

        internal ProcessRobots(RobotsInput robotsInput)
        {
            xBound = robotsInput.xBound;
            yBound = robotsInput.yBound;
            robots = robotsInput.robots;
            lostPositions = new HashSet<int>();
        }

        internal string[] Run()
        {
            string[] output = new string[robots.Count];
            for (int i = 0; i < robots.Count; i++)
            {
                output[i] = ProcessRobot(robots[i]);
            }
            return output;
        }

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
                        if (x < 0 || x > xBound || y < 0 || y > yBound)
                        {
                            // Check if we've already lost a robot here
                            if (lostPositions.Contains(10000 * position.x + position.y))
                            {
                                // Continue to next instruction
                                continue;
                            }
                            else
                            {
                                // Robot is lost
                                isLost = true;
                                lostPositions.Add(10000 * position.x + position.y);
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
