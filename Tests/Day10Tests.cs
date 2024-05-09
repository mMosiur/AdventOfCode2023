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
	[InlineData("my-input.txt", "6613")]
	public void TestPart1(string inputFilename, string expectedResult)
		=> BaseTestPart1(inputFilename, expectedResult);

	[Theory]
	[InlineData("example-input-5.txt", "4")]
	[InlineData("example-input-6.txt", "4")]
	[InlineData("example-input-7.txt", "8")]
	[InlineData("example-input-8.txt", "10")]
	[InlineData("my-input.txt", "511")]
	public void TestPart2(string inputFilename, string expectedResult)
		=> BaseTestPart2(inputFilename, expectedResult);
}
