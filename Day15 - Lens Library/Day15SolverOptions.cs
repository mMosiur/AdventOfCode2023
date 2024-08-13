using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2023.Day15;

public sealed class Day15SolverOptions : DaySolverOptions
{
	public char SequenceSeparator { get; set; } = ',';
	public char RemoveOperationChar { get; set; } = '-';
	public char InsertOperationChar { get; set; } = '=';
}
