using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2023.Day01;

public sealed class Day01Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 1;
	public override string Title => "Trebuchet?!";

	public Day01Solver(Day01SolverOptions options) : base(options)
	{
		// Initialize Day01 solver here.
		// Property `Input` contains the raw input text.
		// Property `InputLines` enumerates lines in the input text.
	}

	public Day01Solver(Action<Day01SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day01Solver() : this(new Day01SolverOptions())
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
