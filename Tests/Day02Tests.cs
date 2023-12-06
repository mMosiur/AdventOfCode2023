using AdventOfCode.Year2023.Day02;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "02")]
[Trait("Day", "2")]
public sealed class Day02Tests : BaseDayTests<Day02Solver, Day02SolverOptions>
{
	protected override string DayInputsDirectory => "Day02";

	protected override Day02Solver CreateSolver(Day02SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "8")]
	[InlineData("my-input.txt", "2164")]
	public void TestPart1(string inputFilename, string expectedResult, Day02SolverOptions? options = null)
		=> BaseTestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "2286")]
	[InlineData("my-input.txt", "69929")]
	public void TestPart2(string inputFilename, string expectedResult, Day02SolverOptions? options = null)
		=> BaseTestPart2(inputFilename, expectedResult, options);
}
