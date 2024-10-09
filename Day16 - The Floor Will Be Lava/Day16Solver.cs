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
		int energizedTileCount = TraverseAndCountEnergizedTiles(traverser, new(0, 0), Direction.Right);
		return energizedTileCount.ToString();
	}

	public override string SolvePart2()
	{
		var traverser = new CaveBeamTraverser(_caveTileGrid);

		int maxEnergizedTileCount =
			GenerateInwardPointingPositions(_caveTileGrid.Bounds)
				.Select(pd => TraverseAndCountEnergizedTiles(traverser, pd.Point, pd.Direction))
				.Max();

		return maxEnergizedTileCount.ToString();
	}

	private static int TraverseAndCountEnergizedTiles(CaveBeamTraverser traverser, Point start, Direction direction)
	{
		var tileBeamDirections = traverser.TraverseWithBeam(start, direction);
		return tileBeamDirections.Cast<DirectionSet>().Count(d => d is not DirectionSet.None);
	}

	private static IEnumerable<(Point Point, Direction Direction)> GenerateInwardPointingPositions(Rectangle<int> bounds)
	{
		// The corners will be covered twice, but that's exactly what we want since
		// in the corners the beam can start in either direction.
		var downBeams = bounds.YRange.Select(col => (new Point(bounds.XRange.Start, col), Direction.Down));
		var leftBeams = bounds.XRange.Select(row => (new Point(row, bounds.YRange.End), Direction.Left));
		var upBeams = bounds.YRange.Select(col => (new Point(bounds.XRange.End, col), Direction.Up));
		var rightBeams = bounds.XRange.Select(row => (new Point(row, bounds.YRange.Start), Direction.Right));

		return downBeams.Concat(leftBeams).Concat(upBeams).Concat(rightBeams);
	}
}
