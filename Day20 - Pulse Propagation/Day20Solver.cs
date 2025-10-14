using AdventOfCode.Common;

namespace AdventOfCode.Year2023.Day20;

public sealed class Day20Solver : DaySolver<Day20SolverOptions>
{
    public override int Year => 2023;
    public override int Day => 20;
    public override string Title => "Pulse Propagation";

    public Day20Solver(Day20SolverOptions options) : base(options)
    {
        // Initialize Day20 solver here.
        // Property `Options` contains options passed to this constructor that were forwarded to the base constructor.
        // Property `Input` contains the raw input text.
        // Property `InputLines` enumerates lines in the input text.
    }

    public Day20Solver(Action<Day20SolverOptions> configure) : this(DaySolverOptions.FromConfigureAction(configure))
    {
    }

    public Day20Solver() : this(new Day20SolverOptions())
    {
    }

    public override string SolvePart1()
    {
        return "UNSOLVED";
    }

    public override string SolvePart2()
    {
        return "UNSOLVED";
    }
}
