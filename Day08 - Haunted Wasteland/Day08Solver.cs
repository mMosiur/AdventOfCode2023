using AdventOfCode.Abstractions;
using AdventOfCode.Year2023.Day08.Input;
using AdventOfCode.Year2023.Day08.Map;

namespace AdventOfCode.Year2023.Day08;

public sealed class Day08Solver : DaySolver
{
	private readonly MapDocuments _mapDocuments;

	public Day08Solver(Day08SolverOptions options) : base(options)
	{
		var inputReader = new InputReader(options.StartNodeLabel, options.EndNodeLabel);
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
		var node = _mapDocuments.StartNode;
		while (node != _mapDocuments.EndNode)
		{
			node = node.NextInPath;
			stepCount += _mapDocuments.Instructions.Count;
		}

		return stepCount.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
