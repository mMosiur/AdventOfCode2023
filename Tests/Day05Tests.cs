using AdventOfCode.Year2023.Day05;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "05")]
[Trait("Day", "5")]
public sealed class Day05Tests : BaseDayTests<Day05Solver, Day05SolverOptions>
{
	protected override string DayInputsDirectory => "Day05";

	protected override Day05Solver CreateSolver(Day05SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "35")]
	[InlineData("my-input.txt", "650599855")]
	public void TestPart1(string inputFilename, string expectedResult)
		=> BaseTestPart1(inputFilename, expectedResult);

	[Theory]
	[InlineData("example-input.txt", "46")]
	[InlineData("my-input.txt", "1240035")]
	public void TestPart2(string inputFilename, string expectedResult)
		=> BaseTestPart2(inputFilename, expectedResult);
}
