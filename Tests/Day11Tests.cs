using AdventOfCode.Year2023.Day11;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "11")]
public sealed class Day11Tests : BaseDayTests<Day11Solver, Day11SolverOptions>
{
	protected override string DayInputsDirectory => "Day11";

	protected override Day11Solver CreateSolver(Day11SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "374")]
	[InlineData("my-input.txt", "10173804")]
	public void TestPart1(string inputFilename, string expectedResult, Day11SolverOptions? options = null)
		=> BaseTestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "", Skip = "Unsolved yet")]
	[InlineData("my-input.txt", "", Skip = "Unsolved yet")]
	public void TestPart2(string inputFilename, string expectedResult, Day11SolverOptions? options = null)
		=> BaseTestPart2(inputFilename, expectedResult, options);
}
