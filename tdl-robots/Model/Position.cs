namespace tdl_robots.Model
{
    /// <summary>
    /// Represents a position, including co-ordinates and direction
    /// </summary>
    internal class Position
    {
        internal Position(int x, int y, int direction)
        {
            this.x = x;
            this.y = y;
            this.direction = direction;
        }
        /// <summary>
        /// Current x co-ordinate
        /// </summary>
        internal int x;
        /// <summary>
        /// Current y co-ordinate
        /// </summary>
        internal int y;
        /// <summary>
        /// Current direction, where 0 = N, 1 = E, 2 = S, 3 = W
        /// </summary>
        internal int direction;
    }
}
