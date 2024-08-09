using System.Text;

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
			TiltToSlideNorthColumn(col);
		}
	}

	private void TiltToSlideNorthColumn(int columnIndex)
	{
		int rowCount = _plane.GetLength(0);
		int? topmostEmpty = null;
		for (int row = 0; row < rowCount; row++)
		{
			var space = _plane[row, columnIndex];
			switch (space)
			{
				case SpaceType.Empty:
					topmostEmpty ??= row;
					break;
				case SpaceType.CubeRock:
					topmostEmpty = null;
					break;
				case SpaceType.RoundRock:
					{
						if (topmostEmpty is not null)
						{
							_plane[topmostEmpty.Value, columnIndex] = SpaceType.RoundRock;
							_plane[row, columnIndex] = SpaceType.Empty;
							topmostEmpty++;
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
}
