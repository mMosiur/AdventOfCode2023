namespace AdventOfCode.Year2023.Day17.Puzzle;

internal static class Directions
{
	public static Vector Right => new(0, 1);
	public static Vector Down => new(1, 0);
	public static Vector Left => new(0, -1);
	public static Vector Up => new(-1, 0);

	public static Vector TurnRight(Vector direction) => new(direction.Y, -direction.X);
	public static Vector TurnLeft(Vector direction) => new(-direction.Y, direction.X);
}
