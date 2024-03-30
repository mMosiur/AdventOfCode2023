using AdventOfCode.Abstractions;
using AdventOfCode.Year2023.Day08.Input;
using AdventOfCode.Year2023.Day08.Map;

namespace AdventOfCode.Year2023.Day08;

public sealed class Day08Solver : DaySolver
{
	private readonly Day08SolverOptions _options;
	private readonly MapDocuments _mapDocuments;

	public Day08Solver(Day08SolverOptions options) : base(options)
	{
		_options = options;
		_mapDocuments = InputReader.Read(InputLines);
	}

	public Day08Solver(Action<Day08SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day08Solver() : this(new Day08SolverOptions())
	{
	}

	public override int Year => 2023;
	public override int Day => 8;
	public override string Title => "Haunted Wasteland";

	public override string SolvePart1()
	{
		string startLabel = _options.PartOneStartNodeLabel;
		string endLabel = _options.PartOneEndNodeLabel;
		_mapDocuments.ReassignNodeTypes(LabelMatchingStrategy.WholeLabel, startLabel, endLabel);

		int steps = CountStepsFromStartToEnd();
		return steps.ToString();
	}

	public override string SolvePart2()
	{
		string startLabel = _options.PartTwoStartNodeLabelSuffix;
		string endLabel = _options.PartTwoEndNodeLabelSuffix;
		_mapDocuments.ReassignNodeTypes(LabelMatchingStrategy.LabelSuffix, startLabel, endLabel);

		int steps = CountStepsFromStartToEnd();
		return steps.ToString();
	}

	private int CountStepsFromStartToEnd()
	{
		int stepCount = 0;
		int stepLength = _mapDocuments.Instructions.Count;
		var nodes = _mapDocuments.Nodes.Where(n => n.IsStart).ToArray();
		bool allEndNodes = nodes.All(n => n.IsEnd);
		while (allEndNodes is false)
		{
			allEndNodes = true;
			for (int i = 0; i < nodes.Length; i++)
			{
				var node = nodes[i].NextInPath;
				nodes[i] = node;
				allEndNodes &= node.IsEnd;
			}

			stepCount++;
		}

		return stepCount * stepLength;
	}
}
