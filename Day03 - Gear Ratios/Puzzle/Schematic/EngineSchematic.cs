namespace AdventOfCode.Year2023.Day03.Puzzle.Schematic;

internal sealed class EngineSchematic(
	IReadOnlyCollection<SchematicNumber> partNumbers,
	IReadOnlyCollection<SchematicSymbol> symbols)
{
	public IReadOnlyCollection<SchematicNumber> PartNumbers { get; } = partNumbers;
	public IReadOnlyCollection<SchematicSymbol> Symbols { get; } = symbols;
}
