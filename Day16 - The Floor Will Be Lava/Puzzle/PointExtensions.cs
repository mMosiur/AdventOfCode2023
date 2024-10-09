namespace AdventOfCode.Year2023.Day16.Puzzle;

internal static class PointExtensions
{
	public static Point Move(this Point point, Direction direction)
		=> direction switch
		{
			Direction.Up => new(point.X - 1, point.Y),
			Direction.Right => new(point.X, point.Y + 1),
			Direction.Down => new(point.X + 1, point.Y),
			Direction.Left => new(point.X, point.Y - 1),
			_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, "Invalid direction."),
		};
}
