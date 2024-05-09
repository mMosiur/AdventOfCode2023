using AdventOfCode.Year2023.Day01;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "01")]
[Trait("Day", "1")]
public sealed class Day01Tests : BaseDayTests<Day01Solver, Day01SolverOptions>
{
	protected override string DayInputsDirectory => "Day01";

	protected override Day01Solver CreateSolver(Day01SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input-1.txt", "142")]
	[InlineData("my-input.txt", "54634")]
	public void TestPart1(string inputFilename, string expectedResult)
		=> BaseTestPart1(inputFilename, expectedResult);

	[Theory]
	[InlineData("example-input-2.txt", "281")]
	[InlineData("my-input.txt", "53855")]
	public void TestPart2(string inputFilename, string expectedResult)
		=> BaseTestPart2(inputFilename, expectedResult);
}
