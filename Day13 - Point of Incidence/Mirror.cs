using System.Text;

namespace AdventOfCode.Year2023.Day13;

internal sealed class Mirror
{
	private readonly Terrain[,] _terrain;
	private readonly int[] _rowHashes;
	private readonly int[] _columnHashes;

	public Mirror(Terrain[,] terrain)
	{
		_terrain = terrain;
		_rowHashes = new int[terrain.GetLength(0)];
		Array.Fill(_rowHashes, -1);
		_columnHashes = new int[terrain.GetLength(1)];
		Array.Fill(_columnHashes, -1);
	}

	public int RowCount => _terrain.GetLength(0);
	public int ColumnCount => _terrain.GetLength(1);

	public IEnumerable<Terrain> GetRow(int row)
	{
		for (int col = 0; col < ColumnCount; col++)
		{
			yield return _terrain[row, col];
		}
	}

	public int GetRowHash(int row)
	{
		int hash = _rowHashes[row];
		if (hash != -1)
		{
			return hash;
		}

		hash = 0;
		for (int col = 0; col < ColumnCount; col++)
		{
			hash <<= 1;
			hash += _terrain[row, col] is Terrain.Rocks ? 1 : 0;
		}

		_rowHashes[row] = hash;
		return hash;
	}

	public IEnumerable<Terrain> GetColumn(int col)
	{
		for (int row = 0; row < RowCount; row++)
		{
			yield return _terrain[row, col];
		}
	}

	public int GetColumnHash(int col)
	{
		int hash = _columnHashes[col];
		if (hash != -1)
		{
			return hash;
		}

		hash = 0;
		for (int row = 0; row < RowCount; row++)
		{
			hash <<= 1;
			hash += _terrain[row, col] is Terrain.Rocks ? 1 : 0;
		}

		_columnHashes[col] = hash;
		return hash;
	}

	public Terrain this[int row, int col] => _terrain[row, col];
}
