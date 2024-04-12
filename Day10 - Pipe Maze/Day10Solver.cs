using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2023.Day10;

public sealed class Day10Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 10;
	public override string Title => "Pipe Maze";

	public Day10Solver(Day10SolverOptions options) : base(options)
	{
		// Initialize Day10 solver here.
		// Property `Input` contains the raw input text.
		// Property `InputLines` enumerates lines in the input text.
	}

	public Day10Solver(Action<Day10SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day10Solver() : this(new Day10SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		return "UNSOLVED";
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
