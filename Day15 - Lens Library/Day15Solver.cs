using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2023.Day15;

public sealed class Day15Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 15;
	public override string Title => "Lens Library";

	public Day15Solver(Day15SolverOptions options) : base(options)
	{
	}

	public Day15Solver(Action<Day15SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day15Solver() : this(new Day15SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		var initializationSequence = InputReader.ReadInitializationSequence(Input);
		var hasher = new Hasher();
		int sum = 0;
		foreach (string str in initializationSequence)
		{
			sum += hasher.Hash(str);
		}
		return sum.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
