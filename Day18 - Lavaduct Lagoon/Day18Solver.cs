using AdventOfCode.Common;

namespace AdventOfCode.Year2023.Day18;

public sealed class Day18Solver : DaySolver<Day18SolverOptions>
{
    public override int Year => 2023;
    public override int Day => 18;
    public override string Title => "Lavaduct Lagoon";

    public Day18Solver(Day18SolverOptions options) : base(options)
    {
        // Initialize Day18 solver here.
        // Property `Options` contains options passed to this constructor that were forwarded to the base constructor.
        // Property `Input` contains the raw input text.
        // Property `InputLines` enumerates lines in the input text.
    }

    public Day18Solver(Action<Day18SolverOptions> configure) : this(DaySolverOptions.FromConfigureAction(configure)) { }

    public Day18Solver() : this(new Day18SolverOptions()) { }

    public override string SolvePart1()
    {
        return "UNSOLVED";
    }

    public override string SolvePart2()
    {
        return "UNSOLVED";
    }
}
