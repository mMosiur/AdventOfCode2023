using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2023.Day03;

public sealed class Day03Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 3;
	public override string Title => "Gear Ratios";

	public Day03Solver(Day03SolverOptions options) : base(options)
	{
		// Initialize Day03 solver here.
		// Property `Input` contains the raw input text.
		// Property `InputLines` enumerates lines in the input text.
	}

	public Day03Solver(Action<Day03SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day03Solver() : this(new Day03SolverOptions())
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
