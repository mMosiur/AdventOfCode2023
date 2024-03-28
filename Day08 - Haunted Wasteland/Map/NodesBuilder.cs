namespace AdventOfCode.Year2023.Day08.Map;

internal sealed class NodesBuilder
{
	private readonly string _endNodeLabel;
	private readonly Dictionary<string, (InternalNode Node, string LeftLabel, string RightLabel)> _nodes;
	private readonly string _startNodeLabel;

	public NodesBuilder(string startNodeLabel, string endNodeLabel)
	{
		_nodes = new();
		_startNodeLabel = startNodeLabel;
		_endNodeLabel = endNodeLabel;
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

			node.SetLeft(leftNodeData.Node);

			if (!_nodes.TryGetValue(rightLabel, out var rightNodeData))
			{
				throw new InvalidOperationException($"Node labeled '{rightLabel}' not found (needed as right node to '{node.Label}')");
			}

			node.SetRight(rightNodeData.Node);
		}

		var nodes = _nodes.Select(kvp => kvp.Value.Node).ToList();
		return (nodes, startNode.Node, endNode.Node);
	}

	private sealed class InternalNode(string label) : Node
	{
		private Node? _left;
		private Node? _right;

		public override string Label { get; } = label;
		public override Node Left => _left!;
		public override Node Right => _right!;

		internal void SetLeft(Node left)
		{
			_left = left;
		}

		internal void SetRight(Node right)
		{
			_right = right;
		}
	}
}
