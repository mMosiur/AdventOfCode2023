using AdventOfCode.Common;

namespace AdventOfCode.Year2023.Day12;

public sealed class Day12Solver : DaySolver<Day12SolverOptions>
{
    public override int Year => 2023;
    public override int Day => 12;
    public override string Title => "Hot Springs";

    private readonly IReadOnlyList<SpringRow> _rows;
    private readonly int _unfoldRepetitions;

    public Day12Solver(Day12SolverOptions options) : base(options)
    {
        var inputReader = new InputReader();
        _rows = inputReader.ReadInput(InputLines);
        _unfoldRepetitions = options.UnfoldRepetitions;
    }

    public Day12Solver(Action<Day12SolverOptions> configure)
        : this(DaySolverOptions.FromConfigureAction(configure)) { }

    public Day12Solver() : this(new Day12SolverOptions()) { }

    public override string SolvePart1()
    {
        var counter = new SpringArrangementCounter();
        long result = 0;
        foreach (var row in _rows)
        {
            result += counter.CountPossibleArrangements(row);
        }

        return result.ToString();
    }

    public override string SolvePart2()
    {
        var counter = new SpringArrangementCounter();
        long result = 0;
        foreach (var row in _rows)
        {
            var unfoldedRow = UnfoldRow(row);
            result += counter.CountPossibleArrangements(unfoldedRow);
        }

        return result.ToString();
    }

    private SpringRow UnfoldRow(SpringRow springRow)
    {
        var newConditionRecords = new SpringCondition[springRow.ConditionRecords.Length * _unfoldRepetitions + _unfoldRepetitions - 1];
        springRow.ConditionRecords.CopyTo(newConditionRecords, 0);
        for (int i = 1; i < _unfoldRepetitions; i++)
        {
            int destination = i * (springRow.ConditionRecords.Length + 1);
            newConditionRecords[destination - 1] = SpringCondition.Unknown;
            springRow.ConditionRecords.CopyTo(newConditionRecords, destination);
        }

        var newGroupSizes = new int[springRow.DamagedGroupSizes.Length * _unfoldRepetitions];
        for (int i = 0; i < _unfoldRepetitions; i++)
        {
            int destination = i * springRow.DamagedGroupSizes.Length;
            springRow.DamagedGroupSizes.CopyTo(newGroupSizes, destination);
        }

        return new(newConditionRecords, newGroupSizes);
    }
}
