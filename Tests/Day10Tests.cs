using AdventOfCode.Year2023.Day10;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "10")]
public sealed class Day10Tests : BaseDayTests<Day10Solver, Day10SolverOptions>
{
	protected override string DayInputsDirectory => "Day10";

	protected override Day10Solver CreateSolver(Day10SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input-1.txt", "4")]
	[InlineData("example-input-2.txt", "4")]
	[InlineData("example-input-3.txt", "8")]
	[InlineData("example-input-4.txt", "8")]
	[InlineData("my-input.txt", "", Skip = "Unsolved yet")]
	public void TestPart1(string inputFilename, string expectedResult, Day10SolverOptions? options = null)
		=> BaseTestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "", Skip = "Unsolved yet")]
	[InlineData("my-input.txt", "", Skip = "Unsolved yet")]
	public void TestPart2(string inputFilename, string expectedResult, Day10SolverOptions? options = null)
		=> BaseTestPart2(inputFilename, expectedResult, options);
}
