using tdl_robots.Data;
using tdl_robots.Model;
using tdl_robots.Services;

try
{
    // Read and parse the input file
    InputParser parser = new();
    RobotsInput input = parser.ParseInput("input.txt");

    // Process the robots and get the output
    ProcessRobots processor = new(input);
    string[] output = processor.Run();

    // Print the output to the console
    foreach (string line in output)
    {
        Console.WriteLine(line);
    }
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}