namespace AdventOfCode.Year2023.Day11.Puzzle;

internal interface IGalaxy
{
	Point Position { get; }
}

internal sealed class Galaxy(Point position) : IGalaxy
{
	public Point Position { get; set; } = position;
}
