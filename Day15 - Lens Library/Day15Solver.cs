using AdventOfCode.Common;

namespace AdventOfCode.Year2023.Day15;

public sealed class Day15Solver : DaySolver<Day15SolverOptions>
{
    public override int Year => 2023;
    public override int Day => 15;
    public override string Title => "Lens Library";

    private readonly InputReader _inputReader;
    private readonly IReadOnlyList<string> _initializationSequence;

    public Day15Solver(Day15SolverOptions options) : base(options)
    {
        _inputReader = new(options);
        _initializationSequence = _inputReader.ReadInitializationSequence(Input);
    }

    public Day15Solver(Action<Day15SolverOptions> configure)
        : this(DaySolverOptions.FromConfigureAction(configure)) { }

    public Day15Solver() : this(new Day15SolverOptions()) { }

    public override string SolvePart1()
    {
        int sumOfHashes = _initializationSequence.Sum(HolidayAsciiStringHelper.Hash);
        return sumOfHashes.ToString();
    }

    public override string SolvePart2()
    {
        var lensConfigurator = new LensConfigurator();
        foreach (string stepString in _initializationSequence)
        {
            var step = _inputReader.ParseStep(stepString);
            lensConfigurator.PerformStep(step);
        }

        int totalFocusingPower = lensConfigurator.CalculateFocusingPower();
        return totalFocusingPower.ToString();
    }
}
