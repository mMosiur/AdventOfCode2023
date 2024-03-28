using AdventOfCode.Year2023.Day07;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "07")]
[Trait("Day", "7")]
public sealed class Day07Tests : BaseDayTests<Day07Solver, Day07SolverOptions>
{
	protected override string DayInputsDirectory => "Day07";

	protected override Day07Solver CreateSolver(Day07SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "6440")]
	[InlineData("my-input.txt", "252052080")]
	public void TestPart1(string inputFilename, string expectedResult, Day07SolverOptions? options = null)
		=> BaseTestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "5905")]
	[InlineData("my-input.txt", "252898370")]
	public void TestPart2(string inputFilename, string expectedResult, Day07SolverOptions? options = null)
		=> BaseTestPart2(inputFilename, expectedResult, options);
}
