using AdventOfCode.Abstractions;
using AdventOfCode.Common.Geometry;
using AdventOfCode.Year2023.Day16.Puzzle;
using AdventOfCode.Year2023.Day16.Puzzle.Cave;

namespace AdventOfCode.Year2023.Day16;

public sealed class Day16Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 16;
	public override string Title => "The Floor Will Be Lava";

	private readonly CaveTileGrid _caveTileGrid;

	public Day16Solver(Day16SolverOptions options) : base(options)
	{
		var grid = InputReader.ReadCaveSpaceGrid(InputLines);
		_caveTileGrid = new(grid);
	}

	public Day16Solver(Action<Day16SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day16Solver() : this(new Day16SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		var traverser = new CaveBeamTraverser(_caveTileGrid);
		var tileBeamDirections = traverser.TraverseWithBeam(new(0, 0), Direction.Right);
		int energizedTileCount = tileBeamDirections.Cast<DirectionSet>().Count(d => d is not DirectionSet.None);
		return energizedTileCount.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
