using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2023.Day08;

public sealed class Day08Solver : DaySolver
{
	public Day08Solver(Day08SolverOptions options) : base(options)
	{
		// Initialize Day08 solver here.
		// Property `Input` contains the raw input text.
		// Property `InputLines` enumerates lines in the input text.
	}

	public Day08Solver(Action<Day08SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day08Solver() : this(new Day08SolverOptions())
	{
	}

	public override int Year => 2023;
	public override int Day => 8;
	public override string Title => "Haunted Wasteland";

	public override string SolvePart1()
	{
		return "UNSOLVED";
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
