using AdventOfCode.Abstractions;
using AdventOfCode.Year2023.Day04.Puzzle;

namespace AdventOfCode.Year2023.Day04;

public sealed class Day04Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 4;
	public override string Title => "Scratchcards";

	private readonly InputReader _inputReader;
	private IReadOnlyList<Scratchcard>? _scratchcards;

	private IReadOnlyList<Scratchcard> Scratchcards => _scratchcards ??= _inputReader.ReadInput(InputLines);

	public Day04Solver(Day04SolverOptions options) : base(options)
	{
		_inputReader = new InputReader();
	}

	public Day04Solver(Action<Day04SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day04Solver() : this(new Day04SolverOptions())
	{
	}

	private static int CalculateCardPoints(Scratchcard scratchcard)
	{
		HashSet<int> winningNumbers = new(scratchcard.WinningNumbers);
		winningNumbers.IntersectWith(scratchcard.MyNumbers);
		int count = winningNumbers.Count;
		if (count == 0)
		{
			return 0;
		}

		return 1 << count - 1;
	}

	public override string SolvePart1()
	{
		int sum = Scratchcards.Sum(CalculateCardPoints);
		return sum.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
