using AdventOfCode.Year2023.Day16;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "16")]
public sealed class Day16Tests : BaseDayTests<Day16Solver, Day16SolverOptions>
{
	protected override string DayInputsDirectory => "Day16";

	protected override Day16Solver CreateSolver(Day16SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "46")]
	[InlineData("my-input.txt", "7951")]
	public void TestPart1(string inputFilename, string expectedResult)
		=> BaseTestPart1(inputFilename, expectedResult);

	[Theory]
	[InlineData("example-input.txt", "51")]
	[InlineData("my-input.txt", "8148")]
	public void TestPart2(string inputFilename, string expectedResult)
		=> BaseTestPart2(inputFilename, expectedResult);
}
