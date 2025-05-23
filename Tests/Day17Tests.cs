using AdventOfCode.Year2023.Day17;

namespace AdventOfCode.Year2023.Tests;

[Trait("Year", "2023")]
[Trait("Day", "17")]
public sealed class Day17Tests : BaseDayTests<Day17Solver, Day17SolverOptions>
{
    protected override string DayInputsDirectory => "Day17";

    protected override Day17Solver CreateSolver(Day17SolverOptions options) => new(options);

    [Theory]
    [InlineData("example-input-1.txt", "102")]
    [InlineData("my-input.txt", "1155")]
    public void TestPart1(string inputFilename, string expectedResult)
        => BaseTestPart1(inputFilename, expectedResult);

    [Theory]
    [InlineData("example-input-1.txt", "94")]
    [InlineData("example-input-2.txt", "71")]
    [InlineData("my-input.txt", "1283")]
    public void TestPart2(string inputFilename, string expectedResult)
        => BaseTestPart2(inputFilename, expectedResult);
}
