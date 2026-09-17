using tdl_robots.Data;
using tdl_robots.Model;
using tdl_robots.Services;

try
{
    InputParser parser = new InputParser();
    RobotsInput input = parser.ParseInput("input.txt");

    ProcessRobots processor = new ProcessRobots(input);
    string[] output = processor.Run();
    foreach (string line in output)
    {
        Console.WriteLine(line);
    }
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}