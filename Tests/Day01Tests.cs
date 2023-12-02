using AdventOfCode.Year2023.Day01;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "01")]
[Trait("Day", "1")]
public class Day01Tests : BaseDayTests<Day01Solver, Day01SolverOptions>
{
	protected override string DayInputsDirectory => "Day01";

	protected override Day01Solver CreateSolver(Day01SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input-1.txt", "142")]
	[InlineData("my-input.txt", "54634")]
	public override void TestPart1(string inputFilename, string expectedResult, Day01SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input-2.txt", "281")]
	[InlineData("my-input.txt", "53855")]
	public override void TestPart2(string inputFilename, string expectedResult, Day01SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
