using AdventOfCode.Common;

namespace AdventOfCode.Year2023.Day20;

public sealed class Day20SolverOptions : DaySolverOptions
{
    public int ButtonPushes { get; set; } = 1000;

    public string ModuleConnectedToButtonName { get; set; } = "broadcaster";

    public string PartTwoFinalModuleName { get; set; } = "rx";
}
