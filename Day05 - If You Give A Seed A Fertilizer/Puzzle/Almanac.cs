namespace AdventOfCode.Year2023.Day05.Puzzle;

internal abstract class Almanac<TSeedNumbersType>
{
	private readonly Dictionary<NumberCategory, NumberMap> _numberMapsBySourceCategory;

	public abstract TSeedNumbersType SeedNumbers { get; }
	public IReadOnlyCollection<NumberMap> NumberMaps { get; }

	protected Almanac(IReadOnlyCollection<NumberMap> numberMaps)
	{
		NumberMaps = numberMaps;
		_numberMapsBySourceCategory = NumberMaps.ToDictionary(nm => nm.SourceCategory);
	}

	public NumberMap this[NumberCategory sourceCategory] => _numberMapsBySourceCategory[sourceCategory];
}
