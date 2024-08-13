using AdventOfCode.Year2023.Day15;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "15")]
public sealed class Day15Tests : BaseDayTests<Day15Solver, Day15SolverOptions>
{
	protected override string DayInputsDirectory => "Day15";

	protected override Day15Solver CreateSolver(Day15SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "1320")]
	[InlineData("my-input.txt", "504449")]
	public void TestPart1(string inputFilename, string expectedResult)
		=> BaseTestPart1(inputFilename, expectedResult);

	[Theory]
	[InlineData("example-input.txt", "145")]
	[InlineData("my-input.txt", "262044")]
	public void TestPart2(string inputFilename, string expectedResult)
		=> BaseTestPart2(inputFilename, expectedResult);
}
