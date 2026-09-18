namespace tdl_robots.Model
{
    /// <summary>
    /// Represents the input data for the robots, including the grid bounds and a list of robots with their starting positions and instructions.
    /// </summary>
    internal class RobotsInput
    {
        /// <summary>
        /// X bound of the grid
        /// </summary>
        internal int xBound;

        /// <summary>
        /// Y bound of the grid
        /// </summary>
        internal int yBound;

        /// <summary>
        /// List of robots, each with their starting position and instructions
        /// </summary>
        internal List<Robot> robots = new();
    }
}
