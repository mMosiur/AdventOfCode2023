using AdventOfCode.Year2023.Day05;
using AdventOfCode.Year2023.Day05.ExtendedMath;
using Range = AdventOfCode.Common.Numerics.Interval<uint>;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "05")]
[Trait("Day", "5")]
public class Day05Tests : BaseDayTests<Day05Solver, Day05SolverOptions>
{
	protected override string DayInputsDirectory => "Day05";

	protected override Day05Solver CreateSolver(Day05SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "35")]
	[InlineData("my-input.txt", "650599855")]
	public override void TestPart1(string inputFilename, string expectedResult, Day05SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "46")]
	// [InlineData("my-input.txt", "UNSOLVED")]
	public override void TestPart2(string inputFilename, string expectedResult, Day05SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);

	#region MultiRange Add

	public static IEnumerable<object[]> MultiRangeAddTestData = new[]
	{
		new object[]
		{
			new Range[] { new(1, 2), new(5, 6), new(9, 11) },
			new Range(0, 7),
			new Range[] { new(0, 7), new(9, 11) }
		},
		new object[]
		{
			new Range[] { new(1, 5), new(7, 10) },
			new Range(12, 15),
			new Range[] { new(1, 5), new(7, 10), new(12, 15) }
		},
		new object[]
		{
			new Range[] { new(1, 3), new(5, 8), new(10, 12) },
			new Range(4, 6),
			new Range[] { new(1, 8), new(10, 12) }
		},
		new object[]
		{
			new Range[] { new(1, 5), new(7, 10) },
			new Range(4, 6),
			new Range[] { new(1, 6), new(7, 10) }
		},
		new object[]
		{
			new Range[] { new(1, 3), new(5, 8) },
			new Range(4, 6),
			new Range[] { new(1, 8) }
		},
	};

	[Theory]
	[MemberData(nameof(MultiRangeAddTestData))]
	public void MultiRangeAddTest(Range[] initialRanges, Range rangeToAdd, Range[] expected)
	{
		MultiRange multiRange = new(initialRanges);

		multiRange.Add(rangeToAdd);

		Assert.True(multiRange.SequenceEqual(expected), $"Initial: ({string.Join(", ", initialRanges)}); Added: {rangeToAdd} Expected: ({string.Join(", ", expected)}); Actual: ({string.Join(", ", multiRange)})");
	}

	#endregion

	#region MultiRange Remove

	public static IEnumerable<object[]> RemoveTestData = new[]
	{
		new object[]
		{
			new Range[] { new(1, 5), new(7, 10), new(12, 15) },
			new Range(4, 8),
			new Range[] { new(1, 3), new(9, 10), new(12, 15) },
			true,
		},
		new object[]
		{
			new Range[] { new(1, 3), new(6, 8) },
			new Range(2, 7),
			new Range[] { new(1, 1), new(8, 8) },
			true,
		},
		new object[]
		{
			new Range[] { new(1, 8), new(10, 12) },
			new Range(3, 5),
			new Range[] { new(1, 2), new(6, 8), new(10, 12) },
			true,
		},
		new object[]
		{
			new Range[] { new(1, 2), new(4, 6), new(10, 15) },
			new Range(7, 12),
			new Range[] { new(1, 2), new(4, 6), new(13, 15) },
			true,
		},
		new object[]
		{
			new Range[] { new(1, 2), new(4, 6), new(10, 15) },
			new Range(7, 9),
			new Range[] { new(1, 2), new(4, 6), new(10, 15) },
			false,
		},
		new object[]
		{
			new Range[] { new(1, 6), new(10, 12) },
			new Range(2, 5),
			new Range[] { new(1, 1), new(6, 6), new(10, 12) },
			true,
		},
		new object[]
		{
			new Range[] { new(1, 6), new(10, 12) },
			new Range(2, 7),
			new Range[] { new(1, 1), new(10, 12) },
			true,
		},
		new object[]
		{
			new Range[] { new(1, 2), new(4, 6), new(10, 12) },
			new Range(2, 11),
			new Range[] { new(1, 1), new(12, 12) },
			true,
		},
	};

	private static bool IsAscending(string s)
	{
		for (int i = 1; i < s.Length; i++)
		{
			if (s[i - 1] > s[i])
			{
				return false;
			}
		}

		return true;
	}

	[Theory]
	[MemberData(nameof(RemoveTestData))]
	public void Remove_ShouldHandleRangesCorrectly(Range[] initialRanges, Range rangeToRemove, Range[] expectedRange, bool expectedReturn)
	{
		MultiRange multiRange = new(initialRanges);

		bool removed = multiRange.Remove(rangeToRemove, out string s);

		// Assert.True(IsAscending(s), s);
		Assert.True(multiRange.SequenceEqual(expectedRange), $"Initial: ({string.Join(", ", initialRanges)}); Removed: {rangeToRemove} Expected: ({string.Join(", ", expectedRange)}); Actual: ({string.Join(", ", multiRange)})");
		Assert.Equal(expectedReturn, removed);
	}

	#endregion
}
