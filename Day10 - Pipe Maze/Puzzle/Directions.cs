namespace AdventOfCode.Year2023.Day10.Puzzle;

internal enum Direction
{
	Up = 0,
	Down = 1,
	Left = 2,
	Right = 3,
}

internal static class Directions
{
	public static Direction Opposite(this Direction direction)
		=> direction switch
		{
			Direction.Up => Direction.Down,
			Direction.Down => Direction.Up,
			Direction.Left => Direction.Right,
			Direction.Right => Direction.Left,
			_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, "Invalid direction.")
		};

	public static Direction RotatedRight(this Direction direction)
		=> direction switch
		{
			Direction.Up => Direction.Right,
			Direction.Right => Direction.Down,
			Direction.Down => Direction.Left,
			Direction.Left => Direction.Up,
			_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, "Invalid direction.")
		};

	public static IEnumerable<Direction> EnumerateRotation(Direction direction, bool skipCurrent = false)
	{
		if (skipCurrent is false)
		{
			yield return direction;
		}

		direction = direction.RotatedRight();
		yield return direction;
		direction = direction.RotatedRight();
		yield return direction;
		direction = direction.RotatedRight();
		yield return direction;
	}
}
