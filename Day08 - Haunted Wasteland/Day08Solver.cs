using AdventOfCode.Abstractions;
using AdventOfCode.Year2023.Day08.Input;
using AdventOfCode.Year2023.Day08.Map;

namespace AdventOfCode.Year2023.Day08;

public sealed class Day08Solver : DaySolver
{
	private readonly MapDocuments _mapDocuments;

	public Day08Solver(Day08SolverOptions options) : base(options)
	{
		var inputReader = new InputReader(options.StartNodeLabelSuffix, options.EndNodeLabelSuffix);
		_mapDocuments = inputReader.Read(InputLines);
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

		int steps = stepCount * stepLength;
		return steps.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
