namespace AdventOfCode.Year2023.Day08.Map;

internal abstract class Node
{
	public abstract string Label { get; }
	public abstract Node Left { get; }
	public abstract Node Right { get; }

	public Node this[Direction direction] =>
		direction switch
		{
			Direction.Left => Left,
			Direction.Right => Right,
			_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
		};
}
