using AdventOfCode.Year2023.Day13;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "13")]
public sealed class Day13Tests : BaseDayTests<Day13Solver, Day13SolverOptions>
{
	protected override string DayInputsDirectory => "Day13";

	protected override Day13Solver CreateSolver(Day13SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "405")]
	[InlineData("my-input.txt", "27505")]
	public void TestPart1(string inputFilename, string expectedResult)
		=> BaseTestPart1(inputFilename, expectedResult);

	[Theory]
	[InlineData("example-input.txt", "400")]
	[InlineData("my-input.txt", "22906")]
	public void TestPart2(string inputFilename, string expectedResult)
		=> BaseTestPart2(inputFilename, expectedResult);
}
