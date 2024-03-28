namespace AdventOfCode.Year2023.Day08.Map;

internal sealed class NodesBuilder
{
	private readonly string _endNodeLabel;
	private readonly IReadOnlyList<Direction> _instruction;
	private readonly Dictionary<string, (InternalNode Node, string LeftLabel, string RightLabel)> _nodes;
	private readonly string _startNodeLabel;

	public NodesBuilder(string startNodeLabel, string endNodeLabel, IReadOnlyList<Direction> instruction)
	{
		_nodes = new();
		_startNodeLabel = startNodeLabel;
		_endNodeLabel = endNodeLabel;
		_instruction = instruction;
	}

	public void AddNode(string label, string leftLabel, string rightLabel)
	{
		var node = new InternalNode(label);
		_nodes.Add(label, (node, leftLabel, rightLabel));
	}

	public (IReadOnlyList<Node> Nodes, Node StartNode, Node EndNode) Build()
	{
		if (!_nodes.TryGetValue(_startNodeLabel, out var startNode))
		{
			throw new InvalidOperationException($"Start node labeled '{_startNodeLabel}' not found");
		}

		if (!_nodes.TryGetValue(_endNodeLabel, out var endNode))
		{
			throw new InvalidOperationException($"End node labeled '{_endNodeLabel}' not found");
		}

		foreach (var (node, leftLabel, rightLabel) in _nodes.Values)
		{
			if (!_nodes.TryGetValue(leftLabel, out var leftNodeData))
			{
				throw new InvalidOperationException($"Node labeled '{leftLabel}' not found (needed as left node to '{node.Label}')");
			}

			node.LeftInternal = leftNodeData.Node;

			if (!_nodes.TryGetValue(rightLabel, out var rightNodeData))
			{
				throw new InvalidOperationException($"Node labeled '{rightLabel}' not found (needed as right node to '{node.Label}')");
			}

			node.RightInternal = rightNodeData.Node;
		}

		var nodes = _nodes.Select(kvp => kvp.Value.Node).ToList();

		// Optimize nodes to have the next node in the path already set as we can only travel in whole paths
		foreach (var node in nodes)
		{
			node.NextInPathInternal = node;
			foreach (var direction in _instruction)
			{
				node.NextInPathInternal = node.NextInPathInternal.GoTo(direction);
			}
		}

		return (nodes, startNode.Node, endNode.Node);
	}

	private sealed class InternalNode(string label) : Node
	{
		internal InternalNode? LeftInternal { get; set; }
		internal InternalNode? RightInternal { get; set; }
		internal InternalNode? NextInPathInternal { get; set; }

		public override string Label { get; } = label;
		public override Node NextInPath => NextInPathInternal!;

		internal InternalNode GoTo(Direction direction)
		{
			return direction switch
			{
				Direction.Left => LeftInternal!,
				Direction.Right => RightInternal!,
				_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
		}
	}
}
