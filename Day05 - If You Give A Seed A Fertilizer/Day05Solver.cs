using AdventOfCode.Common;
using AdventOfCode.Year2023.Day05.Puzzle;

namespace AdventOfCode.Year2023.Day05;

public sealed class Day05Solver : DaySolver<Day05SolverOptions>
{
    public override int Year => 2023;
    public override int Day => 5;
    public override string Title => "If You Give A Seed A Fertilizer";

    private readonly InputReaderWithCaching _inputReader;
    private readonly SeedNumberCategoryCalculator _calculator;

    public Day05Solver(Day05SolverOptions options) : base(options)
    {
        _inputReader = new(Input);
        _calculator = new(NumberCategory.Seed, NumberCategory.Location);
    }

    public Day05Solver(Action<Day05SolverOptions> configure)
        : this(DaySolverOptions.FromConfigureAction(configure)) { }

    public Day05Solver() : this(new Day05SolverOptions()) { }

    public override string SolvePart1()
    {
        var almanac = _inputReader.ReadInputSingleSeeds();
        var locationNumbers = _calculator.CalculateTargetCategoryNumbers(almanac);
        uint minLocationNumber = locationNumbers.Min();
        return minLocationNumber.ToString();
    }

    public override string SolvePart2()
    {
        var almanac = _inputReader.ReadInputSeedRanges();
        var locationNumbers = _calculator.CalculateTargetCategoryNumbers(almanac);
        uint minLocationNumber = locationNumbers.Select(r => r.Start).Min();
        return minLocationNumber.ToString();
    }
}
