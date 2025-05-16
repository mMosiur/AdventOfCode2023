namespace AdventOfCode.Year2023.Day10.Puzzle.Pipes;

internal readonly struct Step
{
    public Direction Direction { get; }
    public Vector Vector { get; }

    private Step(Direction direction, Vector vector)
    {
        Direction = direction;
        Vector = vector;
    }

    public static Step Up => new(Direction.Up, new(-1, 0));
    public static Step Down => new(Direction.Down, new(1, 0));
    public static Step Left => new(Direction.Left, new(0, -1));
    public static Step Right => new(Direction.Right, new(0, 1));

    public static Step FromDirection(Direction direction) => direction switch
    {
        Direction.Up => Up,
        Direction.Down => Down,
        Direction.Left => Left,
        Direction.Right => Right,
        _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, "Invalid direction.")
    };

    public static Point operator +(Point point, Step step) => point + step.Vector;
}
