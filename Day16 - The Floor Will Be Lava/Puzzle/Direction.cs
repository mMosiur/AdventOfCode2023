namespace AdventOfCode.Year2023.Day16;

internal enum Direction
{
	Right = 0b00,
	Down = 0b01,
	Left = 0b10,
	Up = 0b11,
}

internal static class DirectionHelpers
{
	public static Direction Opposite(this Direction direction)
	{
		return (Direction)((int)direction ^ 0b10);
	}

	public static Direction RotateClockwise(this Direction direction)
	{
		return (Direction)(((int)direction + 1) & 0b11);
	}

	public static Direction RotateCounterclockwise(this Direction direction)
	{
		return (Direction)(((int)direction - 1) & 0b11);
	}

	public static bool IsHorizontal(this Direction direction)
	{
		return ((int)direction & 0b01) == 0;
	}

	public static bool IsVertical(this Direction direction)
	{
		return ((int)direction & 0b01) != 0;
	}
}
