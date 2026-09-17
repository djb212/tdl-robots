namespace tdl_robots.Model
{
    internal class Robot
    {
        internal Robot (int xPosition, int yPosition, int direction, string instructions)
        {
            position = new Position(xPosition, yPosition, direction);
            this.instructions = instructions;
        }
        // Initial robot position
        internal Position position;
        // Instructions for this robot. Should consist of the characters L, R, and F only
        internal string instructions;
    }
}
