using AdventOfCode.Common;

namespace AdventOfCode.Year2023.Day19;

public sealed class Day19SolverOptions : DaySolverOptions
{
    public string StartingWorkflowName { get; set; } = "in";
    public string AcceptWorkflowName { get; set; } = "A";
    public string RejectWorkflowName { get; set; } = "R";
}
