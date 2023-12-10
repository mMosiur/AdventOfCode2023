using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2023.Day09;

public sealed class Day09Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 9;
	public override string Title => "Mirage Maintenance";

	public Day09Solver(Day09SolverOptions options) : base(options)
	{
		// Initialize Day09 solver here.
		// Property `Input` contains the raw input text.
		// Property `InputLines` enumerates lines in the input text.
	}

	public Day09Solver(Action<Day09SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day09Solver() : this(new Day09SolverOptions())
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
