using AdventOfCode.Common;

namespace AdventOfCode.Year2023.Day19;

public sealed class Day19Solver : DaySolver<Day19SolverOptions>
{
    public override int Year => 2023;
    public override int Day => 19;
    public override string Title => "Aplenty";

    public Day19Solver(Day19SolverOptions options) : base(options)
    {
        // Initialize Day19 solver here.
        // Property `Options` contains options passed to this constructor that were forwarded to the base constructor.
        // Property `Input` contains the raw input text.
        // Property `InputLines` enumerates lines in the input text.
    }

    public Day19Solver(Action<Day19SolverOptions> configure) : this(DaySolverOptions.FromConfigureAction(configure)) { }

    public Day19Solver() : this(new Day19SolverOptions()) { }

    public override string SolvePart1()
    {
        return "UNSOLVED";
    }

    public override string SolvePart2()
    {
        return "UNSOLVED";
    }
}
