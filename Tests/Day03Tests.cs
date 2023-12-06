using AdventOfCode.Year2023.Day03;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "03")]
[Trait("Day", "3")]
public sealed class Day03Tests : BaseDayTests<Day03Solver, Day03SolverOptions>
{
	protected override string DayInputsDirectory => "Day03";

	protected override Day03Solver CreateSolver(Day03SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "4361")]
	[InlineData("my-input.txt", "529618")]
	public void TestPart1(string inputFilename, string expectedResult, Day03SolverOptions? options = null)
		=> BaseTestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "467835")]
	[InlineData("my-input.txt", "77509019")]
	public void TestPart2(string inputFilename, string expectedResult, Day03SolverOptions? options = null)
		=> BaseTestPart2(inputFilename, expectedResult, options);
}
