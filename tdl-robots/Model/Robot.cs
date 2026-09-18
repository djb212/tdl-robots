namespace tdl_robots.Model
{
    /// <summary>
    /// Represents a robot, including its current position and instructions
    /// </summary>
    internal class Robot
    {
        /// <summary>
        /// Initializes a new instance of the Robot class with the specified position and instructions.
        /// </summary>
        /// <param name="xPosition">Initial x position</param>
        /// <param name="yPosition">Initial y position</param>
        /// <param name="direction">Initial direction</param>
        /// <param name="instructions">Movement instructions</param>
        internal Robot (int xPosition, int yPosition, int direction, string instructions)
        {
            position = new Position(xPosition, yPosition, direction);
            this.instructions = instructions;
        }
        
        /// <summary>
        /// Initial robot position
        /// </summary>
        internal Position position;
        
        /// <summary>
        /// Instructions for this robot. Should consist of the characters L, R, and F only
        /// </summary>
        internal string instructions;
    }
}
