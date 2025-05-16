using System.Diagnostics;
using AdventOfCode.Common;
using AdventOfCode.Year2023.Day07;

try
{
	string? filepath = args.Length switch
	{
		0 => null,
		1 => args[0],
		_ => throw new CommandLineException(
			$"Program was called with too many arguments. Proper usage: \"dotnet run [<input filepath>]\"."
		)
	};

	Day07Solver solver = new(options => { options.InputFilepath = filepath ?? options.InputFilepath; });

	Console.WriteLine($"--- Day {solver.Day}: {solver.Title} ---");

	Console.Write("Part one: ");
	string part1 = solver.SolvePart1();
	Console.WriteLine(part1);

	Console.Write("Part two: ");
	string part2 = solver.SolvePart2();
	Console.WriteLine(part2);
}
catch (AdventOfCodeException e)
{
	string errorPrefix = e switch
	{
		CommandLineException => "Command line error",
		InputException => "Input error",
		DaySolverException => "Day solver error",
		_ => throw new UnreachableException($"Unknown exception type \"{e.GetType()}\".")
	};

	Console.ForegroundColor = ConsoleColor.Red;
	Console.Error.WriteLine($"{errorPrefix}: {e.Message}");
	Console.ResetColor();
	Environment.Exit(1);
}
