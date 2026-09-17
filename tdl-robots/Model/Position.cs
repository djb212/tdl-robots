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
        // Current x co-ordinate
        internal int x;
        // Current y co-ordinate
        internal int y;
        // Current direction, where 0 = N, 1 = E, 2 = S, 3 = W
        internal int direction;
    }
}
