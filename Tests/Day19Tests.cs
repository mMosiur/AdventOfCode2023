using AdventOfCode.Year2023.Day19;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "19")]
public sealed class Day19Tests : BaseDayTests<Day19Solver, Day19SolverOptions>
{
    protected override string DayInputsDirectory => "Day19";

    protected override Day19Solver CreateSolver(Day19SolverOptions options) => new(options);

    [Theory]
    [InlineData("example-input.txt", "19114")]
    [InlineData("my-input.txt", "353046")]
    public void TestPart1(string inputFilename, string expectedResult)
        => BaseTestPart1(inputFilename, expectedResult);

    [Theory]
    [InlineData("example-input.txt", "", Skip = "Unsolved yet")]
    [InlineData("my-input.txt", "", Skip = "Unsolved yet")]
    public void TestPart2(string inputFilename, string expectedResult)
        => BaseTestPart2(inputFilename, expectedResult);
}
