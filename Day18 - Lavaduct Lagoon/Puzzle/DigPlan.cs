namespace AdventOfCode.Year2023.Day18.Puzzle;

internal sealed class DigPlan(IEnumerable<DigPlanRow> rows)
{
    public IReadOnlyList<DigPlanRow> Rows { get; } = rows.ToArray();
}

internal record struct DigPlanRow(
    Direction Direction,
    int Distance,
    Color Color
);
