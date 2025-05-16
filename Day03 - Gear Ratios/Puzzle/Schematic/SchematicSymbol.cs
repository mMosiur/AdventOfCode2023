using AdventOfCode.Common;

namespace AdventOfCode.Year2023.Day03.Puzzle.Schematic;

internal sealed class SchematicSymbol
{
	private ICollection<SchematicNumber>? _adjacentNumbers;

	public required Point Position { get; init; }
	public required char Symbol { get; init; }


	public int AdjacentNumberCount => _adjacentNumbers?.Count ?? 0;

	public ICollection<SchematicNumber> AdjacentNumbers
	{
		get => _adjacentNumbers ??= new List<SchematicNumber>();
		init => _adjacentNumbers = value;
	}

	public int CalculateGearRatio()
	{
		if (AdjacentNumberCount != 2)
		{
			throw new DaySolverException("Gear ratio can only be calculated for symbols with exactly two adjacent numbers.");
		}

		return _adjacentNumbers!.Aggregate(1, (agg, sn) => agg * sn.Value);
	}
}
