namespace AdventOfCode.Year2023.Day05.Puzzle;

internal sealed class AlmanacSeedRanges : Almanac<MultiRange>
{
	public override MultiRange SeedNumbers { get; }

	public AlmanacSeedRanges(MultiRange seedNumbers, IReadOnlyCollection<NumberMap> numberMaps)
		: base(numberMaps)
	{
		SeedNumbers = seedNumbers;
	}
}
