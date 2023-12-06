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

	public static IEnumerable<object[]> MultiRangeTestData()
	{
		yield return new object[]
		{
			new Range[] { new(1, 2), new(5, 6), new(9, 11) },
			new Range(0, 7),
			new Range[] { new(0, 7), new(9, 11) }
		};
		yield return new object[]
		{
			new Range[] { new(1, 5), new(7, 10) },
			new Range(12, 15),
			new Range[] { new(1, 5), new(7, 10), new(12, 15) }
		};
		yield return new object[]
		{
			new Range[] { new(1, 3), new(5, 8), new(10, 12) },
			new Range(4, 6),
			new Range[] { new(1, 3), new(4, 8), new(10, 12) }
		};
		yield return new object[]
		{
			new Range[] { new(1, 5), new(7, 10) },
			new Range(4, 6),
			new Range[] { new(1, 6), new(7, 10) }
		};
		yield return new object[]
		{
			new Range[] { new(1, 3), new(5, 8) },
			new Range(4, 6),
			new Range[] { new(1, 8) }
		};
	}

	[Theory]
	[MemberData(nameof(MultiRangeTestData))]
	public void Add_ShouldHandleRangesCorrectly(Range[] initialRanges, Range rangeToAdd, Range[] expected)
	{
		MultiRange multiRange = new(initialRanges);

		multiRange.Add(rangeToAdd);

		Assert.True(multiRange.SequenceEqual(expected), string.Join(", ", multiRange.AsEnumerable()));
	}
}
