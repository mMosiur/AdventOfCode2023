using AdventOfCode.Year2023.Day08;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "08")]
[Trait("Day", "8")]
public sealed class Day08Tests : BaseDayTests<Day08Solver, Day08SolverOptions>
{
	protected override string DayInputsDirectory => "Day08";

	protected override Day08Solver CreateSolver(Day08SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "UNSOLVED", Skip = "Not implemented yet")]
	[InlineData("my-input.txt", "UNSOLVED", Skip = "Not implemented yet")]
	public void TestPart1(string inputFilename, string expectedResult, Day08SolverOptions? options = null)
		=> BaseTestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "UNSOLVED", Skip = "Not implemented yet")]
	[InlineData("my-input.txt", "UNSOLVED", Skip = "Not implemented yet")]
	public void TestPart2(string inputFilename, string expectedResult, Day08SolverOptions? options = null)
		=> BaseTestPart2(inputFilename, expectedResult, options);
}
