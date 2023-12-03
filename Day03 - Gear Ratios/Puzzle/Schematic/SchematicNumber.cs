namespace AdventOfCode.Year2023.Day03.Puzzle.Schematic;

internal sealed class SchematicNumber(Line position, int value)
{
	public Line Position { get; } = position;
	public int Value { get; } = value;
}
