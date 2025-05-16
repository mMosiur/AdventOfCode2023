using AdventOfCode.Year2023.Day06;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "06")]
[Trait("Day", "6")]
public sealed class Day06Tests : BaseDayTests<Day06Solver, Day06SolverOptions>
{
    protected override string DayInputsDirectory => "Day06";

    protected override Day06Solver CreateSolver(Day06SolverOptions options) => new(options);

    [Theory]
    [InlineData("example-input.txt", "288")]
    [InlineData("my-input.txt", "220320")]
    public void TestPart1(string inputFilename, string expectedResult)
        => BaseTestPart1(inputFilename, expectedResult);

    [Theory]
    [InlineData("example-input.txt", "71503")]
    [InlineData("my-input.txt", "34454850")]
    public void TestPart2(string inputFilename, string expectedResult)
        => BaseTestPart2(inputFilename, expectedResult);
}
