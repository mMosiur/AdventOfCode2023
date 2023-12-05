namespace AdventOfCode.Year2023.Day05.Puzzle;

internal sealed class AlmanacSeedRanges : Almanac<IReadOnlyCollection<Range>>
{
	public override IReadOnlyCollection<Range> SeedNumbers { get; }

	public AlmanacSeedRanges(IReadOnlyCollection<Range> seedNumbers, IReadOnlyCollection<NumberMap> numberMaps)
		: base(numberMaps)
	{
		SeedNumbers = seedNumbers;
	}
}
