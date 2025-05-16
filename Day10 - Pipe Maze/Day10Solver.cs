using AdventOfCode.Common;
using AdventOfCode.Year2023.Day10.Puzzle;
using AdventOfCode.Year2023.Day10.Puzzle.Pipes;

namespace AdventOfCode.Year2023.Day10;

public sealed class Day10Solver : DaySolver<Day10SolverOptions>
{
	private readonly PipeMap _pipeMap;

	public Day10Solver(Day10SolverOptions options) : base(options)
	{
		var mapBuilder = new PipeMapBuilder(options);
		_pipeMap = mapBuilder.BuildFromLines(InputLines);
	}

	public Day10Solver(Action<Day10SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure)) { }

	public Day10Solver() : this(new Day10SolverOptions()) { }

	public override int Year => 2023;
	public override int Day => 10;
	public override string Title => "Pipe Maze";

	public override string SolvePart1()
	{
		var analyzer = new PipeMapAnalyzer(_pipeMap);
		int loopLength = analyzer.GetLoopLength();
		// Since this is a loop then the furthest point will be at half the step count the traverser took
		int furthestPointDistance = loopLength / 2;
		return furthestPointDistance.ToString();
	}

	public override string SolvePart2()
	{
		var analyzer = new PipeMapAnalyzer(_pipeMap);
		int pipeEnclosedArea = analyzer.CalculateEnclosedArea();
		return pipeEnclosedArea.ToString();
	}
}
