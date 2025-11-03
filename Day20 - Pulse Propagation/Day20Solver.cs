using AdventOfCode.Common;
using AdventOfCode.Year2023.Day20.Puzzle;
using AdventOfCode.Year2023.Day20.Puzzle.Models;
using AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

namespace AdventOfCode.Year2023.Day20;

public sealed class Day20Solver : DaySolver<Day20SolverOptions>
{
    public override int Year => 2023;
    public override int Day => 20;
    public override string Title => "Pulse Propagation";

    private readonly MachineModules _modules;

    public Day20Solver(Day20SolverOptions options) : base(options)
    {
        _modules = InputReader.Read(Input, options.ModuleConnectedToButtonName);
    }

    public Day20Solver(Action<Day20SolverOptions> configure) : this(DaySolverOptions.FromConfigureAction(configure))
    {
    }

    public Day20Solver() : this(new Day20SolverOptions())
    {
    }

    public override string SolvePart1()
    {
        var counter = new PulseCounter(_modules);
        var pulses = counter.CountPulses(Options.ButtonPushes, buttonPulse: Pulse.Low);
        var result = pulses.LowPulseCount * pulses.HighPulseCount;
        return result.ToString();
    }

    public override string SolvePart2()
    {
        return "UNSOLVED";
    }
}
