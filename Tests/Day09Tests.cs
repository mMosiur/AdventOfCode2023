using AdventOfCode.Year2023.Day09;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "09")]
[Trait("Day", "9")]
public sealed class Day09Tests : BaseDayTests<Day09Solver, Day09SolverOptions>
{
	protected override string DayInputsDirectory => "Day09";

	protected override Day09Solver CreateSolver(Day09SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "114")]
	[InlineData("my-input.txt", "1969958987")]
	public void TestPart1(string inputFilename, string expectedResult, Day09SolverOptions? options = null)
		=> BaseTestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "2")]
	[InlineData("my-input.txt", "1068")]
	public void TestPart2(string inputFilename, string expectedResult, Day09SolverOptions? options = null)
		=> BaseTestPart2(inputFilename, expectedResult, options);
}
