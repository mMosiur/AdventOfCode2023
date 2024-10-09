namespace AdventOfCode.Year2023.Day16.Puzzle.Cave;

internal enum CaveTile
{
	Empty = 0b000,
	RisingMirror = 0b010,
	FallingMirror = 0b011,
	HorizontalSplitter = 0b100,
	VerticalSplitter = 0b101,
}

internal static class CaveSpaceHelpers
{
	public static bool IsMirror(this CaveTile tile) => ((int)tile & 0b010) != 0;

	public static bool IsSplitter(this CaveTile tile) => ((int)tile & 0b100) != 0;

	public static Direction Reflect(this CaveTile mirror, Direction direction)
		=> mirror switch
		{
			CaveTile.RisingMirror => (Direction)(3 - (int)direction),
			CaveTile.FallingMirror => (Direction)((5 - (int)direction) % 4),
			_ => throw new ArgumentOutOfRangeException(nameof(mirror), mirror, $"Cave space '{mirror}' is not considered a mirror."),
		};

	public static (Direction, Direction?) Split(this CaveTile splitter, Direction direction)
		=> splitter switch
		{
			CaveTile.HorizontalSplitter => direction.IsVertical() ? (Direction.Right, Direction.Left) : (direction, null),
			CaveTile.VerticalSplitter => direction.IsHorizontal() ? (Direction.Down, Direction.Up) : (direction, null),
			_ => throw new ArgumentOutOfRangeException(nameof(splitter), splitter, $"Cave space '{splitter}' is not considered a splitter."),
		};
}
