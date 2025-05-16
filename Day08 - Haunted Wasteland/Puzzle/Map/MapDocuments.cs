using AdventOfCode.Year2023.Day08.Puzzle.Input;

namespace AdventOfCode.Year2023.Day08.Puzzle.Map;

internal sealed class MapDocuments
{
    public required IReadOnlyList<Direction> Instructions { get; init; }
    public required IReadOnlyList<Node> Nodes { get; init; }

    public void ReassignNodeTypes(LabelMatchingStrategy strategy, string startLabel, string endLabel)
    {
        switch (strategy)
        {
            case LabelMatchingStrategy.WholeLabel:
                ReassignNodeTypesWholeLabels(startLabel, endLabel);
                break;
            case LabelMatchingStrategy.LabelSuffix:
                ReassignNodeTypesLabelSuffixes(startLabel, endLabel);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(strategy), strategy, "Invalid label matching strategy");
        }
    }

    private void ReassignNodeTypesWholeLabels(string startLabel, string endLabel)
    {
        foreach (var node in Nodes)
        {
            var startNodeType = node.Label == startLabel ? NodeType.Start : NodeType.Normal;
            var endNodeType = node.Label == endLabel ? NodeType.End : NodeType.Normal;
            node.Type = startNodeType | endNodeType;
        }
    }

    private void ReassignNodeTypesLabelSuffixes(string startLabelSuffix, string endLabelSuffix)
    {
        foreach (var node in Nodes)
        {
            var startNodeType = node.Label.EndsWith(startLabelSuffix) ? NodeType.Start : NodeType.Normal;
            var endNodeType = node.Label.EndsWith(endLabelSuffix) ? NodeType.End : NodeType.Normal;
            node.Type = startNodeType | endNodeType;
        }
    }
}
