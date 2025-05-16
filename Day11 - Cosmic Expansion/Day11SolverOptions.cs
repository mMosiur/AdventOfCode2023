using AdventOfCode.Common;

namespace AdventOfCode.Year2023.Day11;

public sealed class Day11SolverOptions : DaySolverOptions
{
	public char GalaxyChar { get; set; } = '#';

	public int PartOneExpansionMagnitude { get; set; } = 2;
	public int PartTwoExpansionMagnitude { get; set; } = 1_000_000;
}
