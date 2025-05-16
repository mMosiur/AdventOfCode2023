using AdventOfCode.Common;
using AdventOfCode.Common.EnumerableExtensions;
using AdventOfCode.Year2023.Day04.Puzzle;

namespace AdventOfCode.Year2023.Day04;

public sealed class Day04Solver : DaySolver<Day04SolverOptions>
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
        : this(DaySolverOptions.FromConfigureAction(configure)) { }

    public Day04Solver() : this(new Day04SolverOptions()) { }

    private static int CountMyWinningNumbers(Scratchcard scratchcard)
    {
        HashSet<int> winningNumbers = new(scratchcard.WinningNumbers);
        winningNumbers.IntersectWith(scratchcard.MyNumbers);
        return winningNumbers.Count;
    }

    private static int CalculateCardPoints(Scratchcard scratchcard)
    {
        int count = CountMyWinningNumbers(scratchcard);
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
        foreach ((Scratchcard scratchcard, int index) in Scratchcards.WithIndex())
        {
            int count = CountMyWinningNumbers(scratchcard);
            for (int i = 0; i < count; i++)
            {
                Scratchcards[index + i + 1].Copies += scratchcard.Copies;
            }
        }

        int sum = Scratchcards.Sum(sc => sc.Copies);
        return sum.ToString();
    }
}
