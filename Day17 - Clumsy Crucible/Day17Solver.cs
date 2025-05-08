using AdventOfCode.Abstractions;
using AdventOfCode.Year2023.Day17.Puzzle;

namespace AdventOfCode.Year2023.Day17;

public sealed class Day17Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 17;
	public override string Title => "Clumsy Crucible";

	private readonly Day17SolverOptions _options;
	private readonly HeatLossMap _map;

	public Day17Solver(Day17SolverOptions options) : base(options)
	{
		_options = options;
		_map = InputReader.Read(InputLines);
	}

	public Day17Solver(Action<Day17SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure)) { }

	public Day17Solver() : this(new Day17SolverOptions()) { }

	public override string SolvePart1()
	{
		var traverser = new HeatMapCrucibleTraverser(
			heatLossMap: _map,
			startingPoint: new(0, 0),
			_options.CrucibleMaxDistanceStraight
		);
		var finishPoint = new Point(_map.Width - 1, _map.Height - 1);
		int result = traverser.Traverse(finishPoint);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
