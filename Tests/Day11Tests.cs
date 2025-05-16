using AdventOfCode.Year2023.Day11;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "11")]
public sealed class Day11Tests : BaseDayTests<Day11Solver, Day11SolverOptions>
{
    protected override string DayInputsDirectory => "Day11";

    protected override Day11Solver CreateSolver(Day11SolverOptions options) => new(options);

    [Theory]
    [InlineData("example-input.txt", "374")]
    [InlineData("my-input.txt", "10173804")]
    public void TestPart1(string inputFilename, string expectedResult)
        => BaseTestPart1(inputFilename, expectedResult);

    [Theory]
    [InlineData("example-input.txt", "1030", 10)]
    [InlineData("example-input.txt", "8410", 100)]
    [InlineData("my-input.txt", "634324905172")]
    public void TestPart2(string inputFilename, string expectedResult, int? expansionMagnitude = null)
    {
        var options = new Day11SolverOptions();
        if (expansionMagnitude.HasValue)
        {
            options.PartTwoExpansionMagnitude = expansionMagnitude.Value;
        }

        BaseTestPart2(inputFilename, expectedResult, options);
    }
}
