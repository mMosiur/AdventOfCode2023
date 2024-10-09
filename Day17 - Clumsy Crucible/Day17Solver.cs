using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2023.Day17;

public sealed class Day17Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 17;
	public override string Title => "Clumsy Crucible";

	public Day17Solver(Day17SolverOptions options) : base(options)
	{
		// Initialize Day17 solver here.
		// Property `Input` contains the raw input text.
		// Property `InputLines` enumerates lines in the input text.
	}

	public Day17Solver(Action<Day17SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day17Solver() : this(new Day17SolverOptions())
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
