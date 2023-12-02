using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2023.Day02;

public sealed class Day02Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 2;
	public override string Title => "Cube Conundrum";

	public Day02Solver(Day02SolverOptions options) : base(options)
	{
		// Initialize Day02 solver here.
		// Property `Input` contains the raw input text.
		// Property `InputLines` enumerates lines in the input text.
	}

	public Day02Solver(Action<Day02SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day02Solver() : this(new Day02SolverOptions())
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
