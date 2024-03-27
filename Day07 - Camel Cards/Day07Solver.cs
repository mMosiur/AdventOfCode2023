using AdventOfCode.Abstractions;
using AdventOfCode.Year2023.Day07.Puzzle;

namespace AdventOfCode.Year2023.Day07;

public sealed class Day07Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 7;
	public override string Title => "Camel Cards";

	private readonly IReadOnlyCollection<Hand> _hands;

	public Day07Solver(Day07SolverOptions options) : base(options)
	{
		var inputReader = new InputReader();
		_hands = inputReader.Read(InputLines);
	}

	public Day07Solver(Action<Day07SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day07Solver() : this(new Day07SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		var list = _hands.ToList();
		list.Sort(new HandComparer());
		int totalWinnings = list
			.Select((hand, index) => hand.Bid * (index + 1))
			.Sum();
		return totalWinnings.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
