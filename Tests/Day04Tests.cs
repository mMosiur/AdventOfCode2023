using AdventOfCode.Year2023.Day04;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "04")]
[Trait("Day", "4")]
public class Day04Tests : BaseDayTests<Day04Solver, Day04SolverOptions>
{
	protected override string DayInputsDirectory => "Day04";

	protected override Day04Solver CreateSolver(Day04SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "13")]
	[InlineData("my-input.txt", "25231")]
	public override void TestPart1(string inputFilename, string expectedResult, Day04SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	// [Theory]
	// [InlineData("example-input.txt", "UNSOLVED")]
	// [InlineData("my-input.txt", "UNSOLVED")]
	public override void TestPart2(string inputFilename, string expectedResult, Day04SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
