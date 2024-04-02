namespace AdventOfCode.Year2023.Day08.Puzzle.Map;

internal abstract class Node(string label)
{
	private NodeType _type;
	public string Label { get; } = label;
	public abstract Node NextInPath { get; }
	public abstract int PathLength { get; }

	public NodeType Type
	{
		get => _type;

		set
		{
			IsStart = value.HasFlag(NodeType.Start);
			IsEnd = value.HasFlag(NodeType.End);
			_type = value;
		}
	}

	public bool IsStart { get; private set; }
	public bool IsEnd { get; private set; }
}
