namespace AdventOfCode.Year2023.Day08.Puzzle.Map;

internal sealed class NodesBuilder
{
    private readonly Dictionary<string, (InternalNode Node, string LeftLabel, string RightLabel)> _nodes = new();

    public NodesBuilder AddNode(string label, string leftLabel, string rightLabel)
    {
        var node = new InternalNode(label);
        _nodes.Add(label, (node, leftLabel, rightLabel));
        return this;
    }

    public IReadOnlyList<Node> Build(IReadOnlyList<Direction> instructions)
    {
        if (instructions is not { Count: > 0 })
        {
            throw new ArgumentException("No instructions provided", nameof(instructions));
        }

        if (_nodes.Count == 0)
        {
            throw new InvalidOperationException("No nodes defined");
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
            var nextNodeInPath = node;
            foreach (var direction in instructions)
            {
                nextNodeInPath = nextNodeInPath.GoTo(direction);
            }

            node.NextInPathInternal = nextNodeInPath;
            node.PathLengthInternal = instructions.Count;
        }

        return nodes;
    }

    private sealed class InternalNode(string label) : Node(label)
    {
        internal InternalNode? LeftInternal;
        internal InternalNode? NextInPathInternal;
        internal int PathLengthInternal;
        internal InternalNode? RightInternal;

        public override Node NextInPath => NextInPathInternal!;
        public override int PathLength => PathLengthInternal;


        internal InternalNode GoTo(Direction direction)
        {
            return direction switch
            {
                Direction.Left => LeftInternal!,
                Direction.Right => RightInternal!,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        public override string ToString() => $"{Label} ({LeftInternal?.Label ?? "<null>"}, {RightInternal?.Label ?? "<null?"}) [{NextInPathInternal?.Label ?? "<null>"}]";
    }
}
