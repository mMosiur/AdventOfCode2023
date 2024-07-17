using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2023.Day12;

public sealed class Day12SolverOptions : DaySolverOptions
{
	public char OperationalSpringChar { get; set; } = '.';
	public char DamagedSpringChar { get; set; } = '#';
	public char UnknownSpringChar { get; set; } = '?';
}
