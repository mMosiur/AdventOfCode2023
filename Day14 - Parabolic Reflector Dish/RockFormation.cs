namespace AdventOfCode.Year2023.Day14;

internal sealed class RockFormation
{
	private readonly SpaceType[,] _plane;

	public RockFormation(SpaceType[,] plane)
	{
		_plane = plane;
	}

	public void TiltToSlideNorth()
	{
		int columnCount = _plane.GetLength(1);
		for (int col = 0; col < columnCount; col++)
		{
			TiltSingleColumnToSlideNorth(col);
		}
	}

	public void TiltToSlideWest()
	{
		int rowCount = _plane.GetLength(0);
		for (int row = 0; row < rowCount; row++)
		{
			TiltSingleRowToSlideWest(row);
		}
	}

	public void TiltToSlideSouth()
	{
		int columnCount = _plane.GetLength(1);
		for (int col = 0; col < columnCount; col++)
		{
			TiltSingleColumnToSlideSouth(col);
		}
	}

	public void TiltToSlideEast()
	{
		int rowCount = _plane.GetLength(0);
		for (int row = 0; row < rowCount; row++)
		{
			TiltSingleRowToSlideEast(row);
		}
	}

	private void TiltSingleColumnToSlideNorth(int columnIndex)
	{
		int rowCount = _plane.GetLength(0);
		int? topEmpty = null;
		for (int row = 0; row < rowCount; row++)
		{
			var space = _plane[row, columnIndex];
			switch (space)
			{
				case SpaceType.Empty:
					topEmpty ??= row;
					break;
				case SpaceType.CubeRock:
					topEmpty = null;
					break;
				case SpaceType.RoundRock:
					{
						if (topEmpty is not null)
						{
							_plane[topEmpty.Value, columnIndex] = SpaceType.RoundRock;
							_plane[row, columnIndex] = SpaceType.Empty;
							topEmpty++;
						}
						break;
					}
			}
		}
	}

	private void TiltSingleColumnToSlideSouth(int columnIndex)
	{
		int rowCount = _plane.GetLength(0);
		int? bottomEmpty = null;
		for (int row = rowCount - 1; row >= 0; row--)
		{
			var space = _plane[row, columnIndex];
			switch (space)
			{
				case SpaceType.Empty:
					bottomEmpty ??= row;
					break;
				case SpaceType.CubeRock:
					bottomEmpty = null;
					break;
				case SpaceType.RoundRock:
					{
						if (bottomEmpty is not null)
						{
							_plane[bottomEmpty.Value, columnIndex] = SpaceType.RoundRock;
							_plane[row, columnIndex] = SpaceType.Empty;
							bottomEmpty--;
						}

						break;
					}
			}
		}
	}

	private void TiltSingleRowToSlideWest(int rowIndex)
	{
		int columnCount = _plane.GetLength(1);
		int? leftEmpty = null;
		for (int col = 0; col < columnCount; col++)
		{
			var space = _plane[rowIndex, col];
			switch (space)
			{
				case SpaceType.Empty:
					leftEmpty ??= col;
					break;
				case SpaceType.CubeRock:
					leftEmpty = null;
					break;
				case SpaceType.RoundRock:
					{
						if (leftEmpty is not null)
						{
							_plane[rowIndex, leftEmpty.Value] = SpaceType.RoundRock;
							_plane[rowIndex, col] = SpaceType.Empty;
							leftEmpty++;
						}

						break;
					}
			}
		}
	}

	private void TiltSingleRowToSlideEast(int rowIndex)
	{
		int columnCount = _plane.GetLength(1);
		int? rightEmpty = null;
		for (int col = columnCount - 1; col >= 0; col--)
		{
			var space = _plane[rowIndex, col];
			switch (space)
			{
				case SpaceType.Empty:
					rightEmpty ??= col;
					break;
				case SpaceType.CubeRock:
					rightEmpty = null;
					break;
				case SpaceType.RoundRock:
					{
						if (rightEmpty is not null)
						{
							_plane[rowIndex, rightEmpty.Value] = SpaceType.RoundRock;
							_plane[rowIndex, col] = SpaceType.Empty;
							rightEmpty--;
						}

						break;
					}
			}
		}
	}

	public int CalculateTotalLoad()
	{
		int totalLoad = 0;
		int rowCount = _plane.GetLength(0);
		for (int row = 0; row < rowCount; row++)
		{
			int rowRockLoad = rowCount - row;
			for (int col = 0; col < _plane.GetLength(1); col++)
			{
				if (_plane[row, col] is SpaceType.RoundRock)
				{
					totalLoad += rowRockLoad;
				}
			}
		}

		return totalLoad;
	}

	public int GetPlaneHash()
	{
		int hash = 0;
		for (int row = 0; row < _plane.GetLength(0); row++)
		{
			for (int col = 0; col < _plane.GetLength(1); col++)
			{
				hash = HashCode.Combine(hash, _plane[row, col]);
			}
		}

		return hash;
	}
}
