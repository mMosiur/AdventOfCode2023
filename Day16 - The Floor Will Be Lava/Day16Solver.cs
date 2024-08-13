using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2023.Day16;

public sealed class Day16Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 16;
	public override string Title => "The Floor Will Be Lava";

	public Day16Solver(Day16SolverOptions options) : base(options)
	{
		// Initialize Day16 solver here.
		// Property `Input` contains the raw input text.
		// Property `InputLines` enumerates lines in the input text.
	}

	public Day16Solver(Action<Day16SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day16Solver() : this(new Day16SolverOptions())
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
