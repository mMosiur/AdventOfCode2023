using AdventOfCode.Common;

namespace AdventOfCode.Year2023.Day08;

public sealed class Day08SolverOptions : DaySolverOptions
{
    public string PartOneStartNodeLabel { get; set; } = "AAA";
    public string PartOneEndNodeLabel { get; set; } = "ZZZ";

    public string PartTwoStartNodeLabelSuffix { get; set; } = "A";
    public string PartTwoEndNodeLabelSuffix { get; set; } = "Z";
}
