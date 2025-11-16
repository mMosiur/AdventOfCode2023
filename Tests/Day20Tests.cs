using AdventOfCode.Year2023.Day20;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "20")]
public sealed class Day20Tests : BaseDayTests<Day20Solver, Day20SolverOptions>
{
    protected override string DayInputsDirectory => "Day20";

    protected override Day20Solver CreateSolver(Day20SolverOptions options) => new(options);

    [Theory]
    [InlineData("example-input-1.txt", "32000000")]
    [InlineData("example-input-2.txt", "11687500")]
    [InlineData("my-input.txt", "800830848")]
    public void TestPart1(string inputFilename, string expectedResult)
        => BaseTestPart1(inputFilename, expectedResult);

    [Theory]
    [InlineData("my-input.txt", "244055946148853")]
    public void TestPart2(string inputFilename, string expectedResult)
        => BaseTestPart2(inputFilename, expectedResult);
}
