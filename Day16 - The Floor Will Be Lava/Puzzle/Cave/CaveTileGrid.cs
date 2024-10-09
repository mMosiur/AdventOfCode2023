using AdventOfCode.Common.Geometry;

namespace AdventOfCode.Year2023.Day16.Puzzle.Cave;

internal sealed class CaveTileGrid(CaveTile[,] grid)
{
	private readonly CaveTile[,] _grid = grid;

	public int Height => _grid.GetLength(0);
	public int Width => _grid.GetLength(1);
	public Rectangle<int> Bounds => new(0, Height - 1, 0, Width - 1);

	public CaveTile this[Point point] => _grid[point.X, point.Y];
	public CaveTile this[int x, int y] => _grid[x, y];

	public bool IsInBounds(Point point)
	{
		return Bounds.Contains(point);
	}
}
