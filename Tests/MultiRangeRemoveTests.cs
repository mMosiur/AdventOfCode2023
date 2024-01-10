using System.Text;
using AdventOfCode.Year2023.Day05.ExtendedMath;
using Range = AdventOfCode.Common.Numerics.Interval<uint>;

namespace AdventOfCode.Year2023.Tests;

public sealed class MultiRangeRemoveTests
{

	[Theory]
	[MemberData(nameof(RemoveTestData))]
	public void Remove_ShouldHandleRangesCorrectly(Range[] initialRanges, Range rangeToRemove, Range[] expectedRange, bool expectedReturn)
	{
		MultiRange multiRange = new(initialRanges);

		bool removed = multiRange.Remove(rangeToRemove);

		Assert.True(multiRange.SequenceEqual(expectedRange), GenerateErrorMessage(initialRanges, rangeToRemove, expectedRange, multiRange));
		Assert.Equal(expectedReturn, removed);
	}

	public static IEnumerable<object[]> RemoveTestData = new object[][]
	{
		[
			new Range[] { new(1, 5), new(7, 10), new(12, 15) },
			new Range(4, 8),
			new Range[] { new(1, 3), new(9, 10), new(12, 15) },
			true
		],
		[
			new Range[] { new(1, 3), new(6, 8) },
			new Range(2, 7),
			new Range[] { new(1, 1), new(8, 8) },
			true
		],
		[
			new Range[] { new(1, 8), new(10, 12) },
			new Range(3, 5),
			new Range[] { new(1, 2), new(6, 8), new(10, 12) },
			true
		],
		[
			new Range[] { new(1, 2), new(4, 6), new(10, 15) },
			new Range(7, 12),
			new Range[] { new(1, 2), new(4, 6), new(13, 15) },
			true
		],
		[
			new Range[] { new(1, 2), new(4, 6), new(10, 15) },
			new Range(7, 9),
			new Range[] { new(1, 2), new(4, 6), new(10, 15) },
			false
		],
		[
			new Range[] { new(1, 6), new(10, 12) },
			new Range(2, 5),
			new Range[] { new(1, 1), new(6, 6), new(10, 12) },
			true
		],
		[
			new Range[] { new(1, 6), new(10, 12) },
			new Range(2, 7),
			new Range[] { new(1, 1), new(10, 12) },
			true
		],
		[
			new Range[] { new(1, 2), new(4, 6), new(10, 12) },
			new Range(2, 11),
			new Range[] { new(1, 1), new(12, 12) },
			true
		],
		[
			new Range[] { new(1, 5), new(7, 10), new(12, 15) },
			new Range(0, 16),
			new Range[] { },
			true
		],
		[
			new Range[] { new(1, 3), new(6, 8) },
			new Range(0, 0),
			new Range[] { new(1, 3), new(6, 8) },
			false
		],
		[
			new Range[] { new(1, 8), new(10, 12) },
			new Range(9, 9),
			new Range[] { new(1, 8), new(10, 12) },
			false
		],
		[
			new Range[] { new(1, 2), new(4, 6), new(10, 15) },
			new Range(3, 3),
			new Range[] { new(1, 2), new(4, 6), new(10, 15) },
			false
		],
		[
			new Range[] { new(1, 2), new(4, 6), new(10, 15) },
			new Range(0, 16),
			new Range[] { },
			true
		],
		[
			new Range[] { new(1, 6), new(10, 12) },
			new Range(7, 9),
			new Range[] { new(1, 6), new(10, 12) },
			false
		],
		[
			new Range[] { new(1, 6), new(10, 12) },
			new Range(6, 10),
			new Range[] { new(1, 5), new(11, 12) },
			true
		],
		[
			new Range[] { new(1, 2), new(4, 6), new(10, 12) },
			new Range(3, 10),
			new Range[] { new(1, 2), new(11, 12) },
			true
		],
	};

	private static string GenerateErrorMessage(Range[] initialRanges, Range rangeToRemove, Range[] expected, MultiRange actual)
	{
		var sb = new StringBuilder();
		sb.AppendFormat("Initial: ({0}); ", string.Join(", ", initialRanges));
		sb.AppendFormat("Removed: {0}; ", rangeToRemove);
		sb.AppendFormat("Expected: ({0}); ", string.Join(", ", expected));
		sb.AppendFormat("Actual: ({0})", string.Join(", ", actual));
		return sb.ToString();
	}
}
