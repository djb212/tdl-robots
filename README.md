# tdl-robots

By Daniel Baker  
For attention of the team at The Doctors Laboratory

## To run
Simply open the solution in your IDE of choice (Visual Studio recommended), and run the tdl-robots project

## Input
The input will be read from [tdl-robots/input.txt](tdl-robots/input.txt), which can be edited as required. Input is expected to be in the format listed in the specification document, namely:
- The first line should contain two integers between 0 and 50 inclusive, separated by a single whitespace. These represent the maximum permitted co-ordinates
- This should be followed by the input for each robot:
  - A line with two integers representing the initial position, plus a character (either N, E, S or W) representing the inital direction, each separated by a whitespace
  - A line consisting of up to 99 characters (L, R or F) representing the movement instructions

## Output
The output will be written to the console, and again will be in the format listed in the specification, namely two integers representing the final co-ordinates, a character representing the final direction and, if the robot is lost, the word "LOST". Each robot's output will be printed on a new line.

## Use of AI
The code itself was mostly written by hand, although I did ask Copilot for the best way to accomplish some tasks, as well as setting up the config for the main project to be visible to the unit tests. Copilot auto-complete was used for some of the more repetitive parts of the code, as well as the comments for documentation.
