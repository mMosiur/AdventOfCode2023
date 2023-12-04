namespace AdventOfCode.Year2023.Day03.Puzzle.Schematic;

internal readonly struct Line(int row, int startColumn, int endColumn)
{
	public int Row { get; } = row;
	public Range Columns { get; } = new(startColumn, endColumn);
}
