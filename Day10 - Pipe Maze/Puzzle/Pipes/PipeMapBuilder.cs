namespace AdventOfCode.Year2023.Day10.Puzzle.Pipes;

internal sealed class PipeMapBuilder(Day10SolverOptions options)
{
	private readonly Dictionary<char, PipeTile> _tiles = new()
	{
		{ options.VerticalTileChar, PipeTile.Vertical },
		{ options.HorizontalTileChar, PipeTile.Horizontal },
		{ options.NorthEastTileChar, PipeTile.NorthEast },
		{ options.NorthWestTileChar, PipeTile.NorthWest },
		{ options.SouthWestTileChar, PipeTile.SouthWest },
		{ options.SouthEastTileChar, PipeTile.SouthEast },
		{ options.GroundTileChar, PipeTile.Ground },
		{ options.StartingPositionTileChar, PipeTile.StartingPosition }
	};

	public PipeMap BuildFromLines(IEnumerable<string> lines)
	{
		string[] arr = lines.ToArray();
		if (arr.Length == 0)
			throw new ArgumentException("Input lines cannot be empty.", nameof(lines));
		if (arr.Any(s => s.Length != arr[0].Length))
			throw new ArgumentException("Input lines must be of equal length.", nameof(lines));

		var tiles = new PipeTile[arr.Length, arr[0].Length];
		for (int row = 0; row < arr.Length; row++)
		{
			for (int col = 0; col < arr[row].Length; col++)
			{
				tiles[row, col] = ParseCharToTile(arr[row][col]);
			}
		}

		return new(tiles);
	}

	private PipeTile ParseCharToTile(char c)
	{
		if (_tiles.TryGetValue(c, out var tile))
		{
			return tile;
		}

		throw new ArgumentOutOfRangeException(nameof(c), c, "Invalid pipe tile character.");
	}
}
