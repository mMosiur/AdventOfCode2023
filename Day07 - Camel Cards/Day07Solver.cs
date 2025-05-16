using AdventOfCode.Common;
using AdventOfCode.Year2023.Day07.Puzzle.Hand;
using AdventOfCode.Year2023.Day07.Puzzle.Input;

namespace AdventOfCode.Year2023.Day07;

public sealed class Day07Solver : DaySolver<Day07SolverOptions>
{
    public override int Year => 2023;
    public override int Day => 7;
    public override string Title => "Camel Cards";

    private readonly IReadOnlyCollection<InputRow> _inputRows;
    private readonly HandComparer _handComparer = new();

    public Day07Solver(Day07SolverOptions options) : base(options)
    {
        _inputRows = InputReader.Read(InputLines);
    }

    public Day07Solver(Action<Day07SolverOptions> configure)
        : this(DaySolverOptions.FromConfigureAction(configure)) { }

    public Day07Solver() : this(new Day07SolverOptions()) { }

    private List<Hand> BuildHands(bool treatJackAsJoker = false)
    {
        var handBuilder = new HandBuilder(treatJackAsJoker);
        var hands = new List<Hand>(_inputRows.Count);
        foreach (var row in _inputRows)
        {
            handBuilder.Reset();
            var hand = handBuilder
                .WithCards(row.HandRepresentation)
                .WithBid(row.Bid)
                .Build();
            hands.Add(hand);
        }

        return hands;
    }

    private int CalculateTotalWinnings(List<Hand> hands)
    {
        hands.Sort(_handComparer);
        int totalWinnings = hands
            .Select((hand, index) => hand.Bid * (index + 1))
            .Sum();
        return totalWinnings;
    }

    public override string SolvePart1()
    {
        var hands = BuildHands();
        int totalWinnings = CalculateTotalWinnings(hands);
        return totalWinnings.ToString();
    }

    public override string SolvePart2()
    {
        var hands = BuildHands(treatJackAsJoker: true);
        int totalWinnings = CalculateTotalWinnings(hands);
        return totalWinnings.ToString();
    }
}
