using System.Globalization;

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

    public DigInstruction[] GetUnswappedColorInstructions()
    {
        return Rows
            .Select(r => GetFromUnswappedColor(r.ColorCode))
            .ToArray();
    }

    private static DigInstruction GetFromUnswappedColor(ReadOnlySpan<char> colorCode)
    {
        if (colorCode.Length != 6) throw new ArgumentException("Color code must be 6 characters long", nameof(colorCode));
        int distance = int.Parse(colorCode[..5], NumberStyles.HexNumber);
        Direction direction = colorCode[5] switch
        {
            '0' => Direction.Right,
            '1' => Direction.Down,
            '2' => Direction.Left,
            '3' => Direction.Up,
            _ => throw new FormatException($"Invalid direction code: '{colorCode[5]}'")
        };
        return new(direction, distance);
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
