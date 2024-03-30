namespace AdventOfCode.Year2023.Day08.Map;

internal abstract class Node(string label, NodeType type)
{
	public  string Label { get; } = label;
	public abstract Node NextInPath { get; }
	public NodeType Type { get; } = type;

	public bool IsStart => Type.HasFlag(NodeType.Start);
	public bool IsEnd => Type.HasFlag(NodeType.End);
}
