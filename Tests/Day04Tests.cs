using AdventOfCode.Year2023.Day04;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "04")]
[Trait("Day", "4")]
public sealed class Day04Tests : BaseDayTests<Day04Solver, Day04SolverOptions>
{
	protected override string DayInputsDirectory => "Day04";

	protected override Day04Solver CreateSolver(Day04SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "13")]
	[InlineData("my-input.txt", "25231")]
	public void TestPart1(string inputFilename, string expectedResult)
		=> BaseTestPart1(inputFilename, expectedResult);

	[Theory]
	[InlineData("example-input.txt", "30")]
	[InlineData("my-input.txt", "9721255")]
	public void TestPart2(string inputFilename, string expectedResult)
		=> BaseTestPart2(inputFilename, expectedResult);
}
