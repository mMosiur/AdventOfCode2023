namespace AdventOfCode.Year2023.Day08.Map;

internal abstract class Node
{
	public abstract string Label { get; }
	public abstract Node NextInPath { get; }
}
