using AdventOfCode.Year2023.Day03;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "03")]
[Trait("Day", "3")]
public class Day03Tests : BaseDayTests<Day03Solver, Day03SolverOptions>
{
	protected override string DayInputsDirectory => "Day03";

	protected override Day03Solver CreateSolver(Day03SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "4361")]
	// [InlineData("my-input.txt", "UNSOLVED")]
	public override void TestPart1(string inputFilename, string expectedResult, Day03SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	// [Theory]
	// [InlineData("example-input.txt", "UNSOLVED")]
	// [InlineData("my-input.txt", "UNSOLVED")]
	public override void TestPart2(string inputFilename, string expectedResult, Day03SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
