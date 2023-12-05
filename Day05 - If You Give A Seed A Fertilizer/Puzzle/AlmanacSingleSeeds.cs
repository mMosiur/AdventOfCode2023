namespace AdventOfCode.Year2023.Day05.Puzzle;

internal sealed class AlmanacSingleSeeds : Almanac<IReadOnlyCollection<uint>>
{
	public override IReadOnlyCollection<uint> SeedNumbers { get; }

	public AlmanacSingleSeeds(IReadOnlyCollection<uint> seedNumbers, IReadOnlyCollection<NumberMap> numberMaps)
		: base(numberMaps)
	{
		SeedNumbers = seedNumbers;
	}
}
