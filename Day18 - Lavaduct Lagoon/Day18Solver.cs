using AdventOfCode.Common;
using AdventOfCode.Year2023.Day18.Puzzle;

namespace AdventOfCode.Year2023.Day18;

public sealed class Day18Solver : DaySolver<Day18SolverOptions>
{
    public override int Year => 2023;
    public override int Day => 18;
    public override string Title => "Lavaduct Lagoon";

    private readonly DigPlan _digPlan;

    public Day18Solver(Day18SolverOptions options) : base(options)
    {
        _digPlan = InputReader.Read(Input);
    }

    public Day18Solver(Action<Day18SolverOptions> configure) : this(DaySolverOptions.FromConfigureAction(configure)) { }

    public Day18Solver() : this(new Day18SolverOptions()) { }

    public override string SolvePart1()
    {
        var digInstructions = _digPlan.GetPlainInstructions();
        var digger = new Digger(digInstructions);
        long digSize = digger.CalculateDigSize(startingPoint: Point.Origin);

        return digSize.ToString();
    }

    public override string SolvePart2()
    {
        var digInstructions = _digPlan.GetUnswappedColorInstructions();
        var digger = new Digger(digInstructions);
        long digSize = digger.CalculateDigSize(startingPoint: Point.Origin);

        return digSize.ToString();
    }
}
