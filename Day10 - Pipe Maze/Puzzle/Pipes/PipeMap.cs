namespace AdventOfCode.Year2023.Day10.Puzzle.Pipes;

internal sealed class PipeMap
{
	private readonly PipeTile[,] _tiles;

	public PipeMap(PipeTile[,] tiles)
	{
		_tiles = tiles;
		RowCount = tiles.GetLength(0);
		ColCount = tiles.GetLength(1);
		Start = GetPositionOfStartTile();
	}

	public Point Start { get; }

	public int RowCount { get; }
	public int ColCount { get; }

	public PipeTile this[int row, int col] => _tiles[row, col];
	public PipeTile this[Point p] => this[p.X, p.Y];

	public bool ContainsPoint(Point p)
	{
		return p.X >= 0 && p.X < RowCount && p.Y >= 0 && p.Y < ColCount;
	}

	public bool TryGetPoint(Point p, out PipeTile tile)
	{
		if (!ContainsPoint(p))
		{
			tile = default;
			return false;
		}

		tile = this[p];
		return true;
	}

	private Point GetPositionOfStartTile()
	{
		Point? position = null;
		for (int row = 0; row < RowCount; row++)
		{
			for (int col = 0; col < ColCount; col++)
			{
				if (_tiles[row, col] is PipeTile.StartingPosition)
				{
					position = position.HasValue
						? throw new InvalidOperationException("Multiple starting positions found in input.")
						: new(row, col);
				}
			}
		}

		return position ?? throw new InvalidOperationException($"Starting character '{(char)PipeTile.StartingPosition}' not found in input.");
	}

	public PipeMapTraverser CreateTraverser(Point? startPoint = null)
	{
		startPoint ??= Start;
		return new(this, startPoint.Value);
	}
}
