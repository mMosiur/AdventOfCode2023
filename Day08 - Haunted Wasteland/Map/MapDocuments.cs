namespace AdventOfCode.Year2023.Day08.Map;

internal sealed class MapDocuments
{
	public required IReadOnlyList<Direction> Instructions { get; init; }
	public required IReadOnlyList<Node> Nodes { get; init; }
	public required Node StartNode { get; init; }
	public required Node EndNode { get; init; }
}