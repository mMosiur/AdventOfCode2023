namespace AdventOfCode.Year2023.Day16;

[Flags]
internal enum DirectionSet
{
	None = 0b0000,

	Right = 0b0001,
	Down = 0b0010,
	Left = 0b0100,
	Up = 0b1000,

	Vertical = Down | Up,
	Horizontal = Right | Left,
	All = Right | Down | Left | Up,
}


internal static class DirectionSetHelpers
{
	public static DirectionSet ToDirectionSet(this Direction direction)
	{
		return (DirectionSet)(1 << (int)direction);
	}

	public static bool Contains(this DirectionSet set, DirectionSet directions)
	{
		return (set & directions) == directions;
	}

	public static bool Contains(this DirectionSet set, Direction direction)
	{
		return set.Contains(direction.ToDirectionSet());
	}

	public static DirectionSet With(this DirectionSet set, DirectionSet directions)
	{
		return set | directions;
	}

	public static DirectionSet With(this DirectionSet set, Direction direction)
	{
		return set.With(direction.ToDirectionSet());
	}
}
