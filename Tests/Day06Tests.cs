using AdventOfCode.Year2023.Day06;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "06")]
[Trait("Day", "6")]
public sealed class Day06Tests : BaseDayTests<Day06Solver, Day06SolverOptions>
{
	protected override string DayInputsDirectory => "Day06";

	protected override Day06Solver CreateSolver(Day06SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "288")]
	[InlineData("my-input.txt", "UNSOLVED", Skip = "Not solved yet")]
	public void TestPart1(string inputFilename, string expectedResult, Day06SolverOptions? options = null)
		=> BaseTestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "UNSOLVED", Skip = "Not known yet")]
	[InlineData("my-input.txt", "UNSOLVED", Skip = "Not solved yet")]
	public void TestPart2(string inputFilename, string expectedResult, Day06SolverOptions? options = null)
		=> BaseTestPart2(inputFilename, expectedResult, options);
}
