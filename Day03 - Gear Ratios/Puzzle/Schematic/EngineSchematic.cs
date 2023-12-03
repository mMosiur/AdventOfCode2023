namespace AdventOfCode.Year2023.Day03.Puzzle.Schematic;

internal sealed class EngineSchematic
{
	private readonly HashSet<Point> _symbolPositions;
	private readonly List<SchematicNumber> _numbers;

	public IReadOnlySet<Point> SymbolPositions => _symbolPositions;
	public IReadOnlyList<SchematicNumber> Numbers => _numbers;

	public EngineSchematic(HashSet<Point> symbolPositions, List<SchematicNumber> numbers)
	{
		_symbolPositions = symbolPositions;
		_numbers = numbers;
	}
}
