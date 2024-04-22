using AdventOfCode.Abstractions;
using AdventOfCode.Year2023.Day10.Puzzle.Pipes;

namespace AdventOfCode.Year2023.Day10;

public sealed class Day10Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 10;
	public override string Title => "Pipe Maze";

	private readonly PipeMapBuilder _mapBuilder;

	public Day10Solver(Day10SolverOptions options) : base(options)
	{
		try
		{
			_mapBuilder = new(options);
		}
		catch (Exception e)
		{
			throw new InputException("Invalid day solver options", e);
		}
	}

	public Day10Solver(Action<Day10SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day10Solver() : this(new Day10SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		var map = _mapBuilder.BuildFromLines(InputLines);
		var traverser = map.CreateTraverser();

		do
		{
			traverser.TakeStep();
		} while (traverser.CurrentPoint != map.Start);

		// Since this is a loop then the furthest point will be at half the step count the traverser took
		int furthestPointDistance = traverser.StepCount / 2;

		return furthestPointDistance.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
