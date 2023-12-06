using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2023.Day06;

public sealed class Day06Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 6;
	public override string Title => "Wait For It";

	public Day06Solver(Day06SolverOptions options) : base(options)
	{
		// Initialize Day06 solver here.
		// Property `Input` contains the raw input text.
		// Property `InputLines` enumerates lines in the input text.
	}

	public Day06Solver(Action<Day06SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day06Solver() : this(new Day06SolverOptions())
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
