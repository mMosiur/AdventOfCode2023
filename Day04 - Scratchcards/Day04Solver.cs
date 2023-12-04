using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2023.Day04;

public sealed class Day04Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 4;
	public override string Title => "UNKNOWN";

	public Day04Solver(Day04SolverOptions options) : base(options)
	{
		// Initialize Day04 solver here.
		// Property `Input` contains the raw input text.
		// Property `InputLines` enumerates lines in the input text.
	}

	public Day04Solver(Action<Day04SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day04Solver() : this(new Day04SolverOptions())
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
