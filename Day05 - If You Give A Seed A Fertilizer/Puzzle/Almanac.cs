namespace AdventOfCode.Year2023.Day05.Puzzle;

internal sealed class Almanac
{
	private readonly Dictionary<NumberCategory, NumberMap> _numberMapsBySourceCategory;

	public IReadOnlyList<uint> SeedNumbers { get; }
	public IReadOnlyCollection<NumberMap> NumberMaps { get; }

	public Almanac(IReadOnlyList<uint> seedNumbers, IReadOnlyCollection<NumberMap> numberMaps)
	{
		SeedNumbers = seedNumbers;
		NumberMaps = numberMaps;
		_numberMapsBySourceCategory = NumberMaps.ToDictionary(nm => nm.SourceCategory);
	}

	public NumberMap this[NumberCategory sourceCategory] => _numberMapsBySourceCategory[sourceCategory];
}
