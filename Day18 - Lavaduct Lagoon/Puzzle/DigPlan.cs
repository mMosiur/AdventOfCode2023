namespace AdventOfCode.Year2023.Day18.Puzzle;

internal sealed class DigPlan(IEnumerable<DigPlanRow> rows)
{
    public IReadOnlyList<DigPlanRow> Rows { get; } = rows.ToArray();

    public DigInstruction[] GetPlainInstructions()
    {
        return Rows
            .Select(r => new DigInstruction(r.Direction, r.Distance))
            .ToArray();
    }
}

internal readonly record struct DigPlanRow(
    Direction Direction,
    int Distance,
    string ColorCode);

internal readonly record struct DigInstruction(
    Direction Direction,
    int Distance)
{
    public Vector DigVector => Direction.ToVector() * Distance;
}
