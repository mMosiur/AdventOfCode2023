namespace AdventOfCode.Year2023.Day05.Puzzle;

internal sealed class SeedNumberCategoryCalculator
{
	private readonly NumberCategory _initialCategory;
	private readonly NumberCategory _targetCategory;

	public SeedNumberCategoryCalculator(NumberCategory initialCategory, NumberCategory targetCategory)
	{
		_initialCategory = initialCategory;
		_targetCategory = targetCategory;
	}

	public IReadOnlyCollection<uint> CalculateTargetCategoryNumbers(AlmanacSingleSeeds almanac)
	{
		uint[] numbers = almanac.SeedNumbers.ToArray();
		var currentNumberCategory = _initialCategory;
		while (currentNumberCategory != _targetCategory)
		{
			var numberMap = almanac[currentNumberCategory];
			for (int i = 0; i < numbers.Length; i++)
			{
				numbers[i] = numberMap.ConvertNumber(numbers[i]);
			}
			currentNumberCategory = numberMap.DestinationCategory;
		}

		return numbers;
	}

	public IReadOnlyCollection<Range> CalculateTargetCategoryNumbers(AlmanacSeedRanges almanac)
	{
		throw new NotImplementedException();
	}
}
