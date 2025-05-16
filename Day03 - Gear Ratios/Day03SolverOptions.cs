using AdventOfCode.Common;

namespace AdventOfCode.Year2023.Day03;

public sealed class Day03SolverOptions : DaySolverOptions
{
    public char IgnoredSymbol { get; set; } = '.';
    public char GearSymbol { get; set; } = '*';
    public int GearAdjacentPartNumberCount { get; set; } = 2;
}
