using System.Text;
using AdventOfCode.Year2023.Day05.ExtendedMath;
using Range = AdventOfCode.Common.Numerics.Interval<uint>;

namespace AdventOfCode.Year2023.Tests;

public sealed class MultiRangeAddTests
{
	[Theory]
	[MemberData(nameof(MultiRangeAddTestData))]
	public void MultiRangeAddTest(Range[] initialRanges, Range rangeToAdd, Range[] expected)
	{
		MultiRange multiRange = new(initialRanges);

		multiRange.Add(rangeToAdd);

		Assert.True(multiRange.SequenceEqual(expected), GenerateErrorMessage(initialRanges, rangeToAdd, expected, multiRange));
	}

	public static IEnumerable<object[]> MultiRangeAddTestData = new object[][]
	{
		[
			new Range[] { new(1, 2), new(5, 6), new(9, 11) },
			new Range(0, 7),
			new Range[] { new(0, 7), new(9, 11) }
		],
		[
			new Range[] { new(1, 5), new(7, 10) },
			new Range(12, 15),
			new Range[] { new(1, 5), new(7, 10), new(12, 15) }
		],
		[
			new Range[] { new(1, 3), new(5, 8), new(10, 12) },
			new Range(4, 6),
			new Range[] { new(1, 8), new(10, 12) }
		],
		[
			new Range[] { new(1, 5), new(7, 10) },
			new Range(4, 6),
			new Range[] { new(1, 6), new(7, 10) }
		],
		[
			new Range[] { new(1, 3), new(5, 8) },
			new Range(4, 6),
			new Range[] { new(1, 8) }
		],
	};

	private static string GenerateErrorMessage(Range[] initialRanges, Range rangeToAdd, Range[] expected, MultiRange actual)
	{
		var sb = new StringBuilder();
		sb.AppendFormat("Initial: ({0}); ", string.Join(", ", initialRanges));
		sb.AppendFormat("Added: {0}; ", rangeToAdd);
		sb.AppendFormat("Expected: ({0}); ", string.Join(", ", expected));
		sb.AppendFormat("Actual: ({0})", string.Join(", ", actual));
		return sb.ToString();
	}
}
