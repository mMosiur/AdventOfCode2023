using AdventOfCode.Year2023.Day12;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "12")]
public sealed class Day12Tests : BaseDayTests<Day12Solver, Day12SolverOptions>
{
	protected override string DayInputsDirectory => "Day12";

	protected override Day12Solver CreateSolver(Day12SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "21")]
	[InlineData("my-input.txt", "", Skip = "Unsolved yet")]
	public void TestPart1(string inputFilename, string expectedResult)
		=> BaseTestPart1(inputFilename, expectedResult);

	[Theory]
	[InlineData("example-input.txt", "", Skip = "Unsolved yet")]
	[InlineData("my-input.txt", "", Skip = "Unsolved yet")]
	public void TestPart2(string inputFilename, string expectedResult)
		=> BaseTestPart2(inputFilename, expectedResult);
}
