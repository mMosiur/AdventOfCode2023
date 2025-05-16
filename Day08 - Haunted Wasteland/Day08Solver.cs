using AdventOfCode.Common;
using AdventOfCode.Year2023.Day08.Puzzle.Input;
using AdventOfCode.Year2023.Day08.Puzzle.Map;

namespace AdventOfCode.Year2023.Day08;

public sealed class Day08Solver : DaySolver<Day08SolverOptions>
{
	private readonly MapDocuments _mapDocuments;
	private readonly Day08SolverOptions _options;

	public Day08Solver(Day08SolverOptions options) : base(options)
	{
		_options = options;
		_mapDocuments = InputReader.Read(InputLines);
	}

	public Day08Solver(Action<Day08SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure)) { }

	public Day08Solver() : this(new Day08SolverOptions()) { }

	public override int Year => 2023;
	public override int Day => 8;
	public override string Title => "Haunted Wasteland";

	public override string SolvePart1()
	{
		string startLabel = _options.PartOneStartNodeLabel;
		string endLabel = _options.PartOneEndNodeLabel;
		_mapDocuments.ReassignNodeTypes(LabelMatchingStrategy.WholeLabel, startLabel, endLabel);

		var startNode = _mapDocuments.Nodes.First(n => n.IsStart);
		ulong steps = CountStepsFromStartToEnd(startNode);
		return steps.ToString();
	}

	public override string SolvePart2()
	{
		string startLabel = _options.PartTwoStartNodeLabelSuffix;
		string endLabel = _options.PartTwoEndNodeLabelSuffix;
		_mapDocuments.ReassignNodeTypes(LabelMatchingStrategy.LabelSuffix, startLabel, endLabel);

		var startNodes = _mapDocuments.Nodes.Where(n => n.IsStart).ToArray();
		ulong steps = CountStepsFromStartToEnd(startNodes);
		return steps.ToString();
	}

	private static ulong CountStepsFromStartToEnd(Node node)
	{
		ulong nodeStepCount = 0;
		while (node.IsEnd is false)
		{
			nodeStepCount += (ulong)node.PathLength;
			node = node.NextInPath;
		}

		return nodeStepCount;
	}

	private static ulong CountStepsFromStartToEnd(IReadOnlyCollection<Node> nodes)
	{
		if (nodes.Count == 0)
		{
			return 0;
		}

		var nodeStepCounts = nodes
			.Select(CountStepsFromStartToEnd);

		return MathExtended.LeastCommonMultiple(nodeStepCounts);
	}
}
