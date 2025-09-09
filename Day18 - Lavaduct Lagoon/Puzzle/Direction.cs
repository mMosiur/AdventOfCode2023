namespace AdventOfCode.Year2023.Day18.Puzzle;

internal enum Direction
{
    Up = 1,
    Down = 2,
    Left = 3,
    Right = 4,
}

internal static class DirectionHelpers
{
    public static Direction Parse(ReadOnlySpan<char> s)
        => s.Trim() switch
        {
            "U" => Direction.Up,
            "D" => Direction.Down,
            "L" => Direction.Left,
            "R" => Direction.Right,
            _ => throw new ArgumentException($"Invalid direction: '{s.ToString()}'")
        };

    public static Vector ToVector(this Direction direction)
        => direction switch
        {
            Direction.Up => new(-1, 0),
            Direction.Down => new(1, 0),
            Direction.Left => new(0, -1),
            Direction.Right => new(0, 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), $"Invalid direction: {direction}")
        };
}
