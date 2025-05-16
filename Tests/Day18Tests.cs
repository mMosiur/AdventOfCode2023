using AdventOfCode.Year2023.Day18;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "18")]
public sealed class Day18Tests : BaseDayTests<Day18Solver, Day18SolverOptions>
{
    protected override string DayInputsDirectory => "Day18";

    protected override Day18Solver CreateSolver(Day18SolverOptions options) => new(options);

    [Theory]
    [InlineData("example-input.txt", "62")]
    [InlineData("my-input.txt", "", Skip = "Unsolved yet")]
    public void TestPart1(string inputFilename, string expectedResult)
        => BaseTestPart1(inputFilename, expectedResult);

    [Theory]
    [InlineData("example-input.txt", "", Skip = "Unsolved yet")]
    [InlineData("my-input.txt", "", Skip = "Unsolved yet")]
    public void TestPart2(string inputFilename, string expectedResult)
        => BaseTestPart2(inputFilename, expectedResult);
}
