using AdventOfCode.Year2023.Day14;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "14")]
public sealed class Day14Tests : BaseDayTests<Day14Solver, Day14SolverOptions>
{
	protected override string DayInputsDirectory => "Day14";

	protected override Day14Solver CreateSolver(Day14SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "136")]
	[InlineData("my-input.txt", "106648")]
	public void TestPart1(string inputFilename, string expectedResult)
		=> BaseTestPart1(inputFilename, expectedResult);

	[Theory]
	[InlineData("example-input.txt", "64")]
	[InlineData("my-input.txt", "87700")]
	public void TestPart2(string inputFilename, string expectedResult)
		=> BaseTestPart2(inputFilename, expectedResult);
}
