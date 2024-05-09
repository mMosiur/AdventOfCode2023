namespace AdventOfCode.Year2023.Day11.Puzzle;

internal sealed class GalaxyMap
{
	private const int ExpandListInitialCapacity = 10;

	private readonly List<Galaxy> _galaxies;
	private long _maxColumn;
	private long _maxRow;

	public GalaxyMap(IEnumerable<Point> galaxyPositions)
	{
		_galaxies = galaxyPositions.Select(p => new Galaxy(p)).ToList();
		_maxRow = _galaxies.Max(g => g.Position.X);
		_maxColumn = _galaxies.Max(g => g.Position.Y);
	}

	public IReadOnlyList<IGalaxy> Galaxies => _galaxies;

	public void Expand(int expansionMagnitude)
	{
		ExpandRows(expansionMagnitude);
		ExpandColumns(expansionMagnitude);
	}

	private void ExpandRows(int expansionMagnitude)
	{
		var galaxiesPerRow = new List<Galaxy>?[_maxRow + 1];
		foreach (var galaxy in _galaxies)
		{
			long row = galaxy.Position.X;
			var rowList = galaxiesPerRow[row];
			if (rowList is null)
			{
				rowList = new List<Galaxy>(ExpandListInitialCapacity);
				galaxiesPerRow[row] = rowList;
			}

			rowList.Add(galaxy);
		}

		int rowOffset = 0;
		for (int row = 0; row <= _maxRow; row++)
		{
			var rowList = galaxiesPerRow[row];
			if (rowList is null)
			{
				rowOffset = rowOffset + expansionMagnitude - 1;
				continue;
			}

			foreach (var galaxy in rowList)
			{
				galaxy.Position = new(row + rowOffset, galaxy.Position.Y);
			}
		}

		_maxRow += rowOffset;
	}

	private void ExpandColumns(int expansionMagnitude)
	{
		var galaxiesPerColumn = new List<Galaxy>?[_maxColumn + 1];
		foreach (var galaxy in _galaxies)
		{
			long column = galaxy.Position.Y;
			var columnList = galaxiesPerColumn[column];
			if (columnList is null)
			{
				columnList = new List<Galaxy>(ExpandListInitialCapacity);
				galaxiesPerColumn[column] = columnList;
			}

			columnList.Add(galaxy);
		}

		int columnOffset = 0;
		for (int column = 0; column <= _maxColumn; column++)
		{
			var columnList = galaxiesPerColumn[column];
			if (columnList is null)
			{
				columnOffset = columnOffset + expansionMagnitude - 1;
				continue;
			}

			foreach (var galaxy in columnList)
			{
				galaxy.Position = new(galaxy.Position.X, column + columnOffset);
			}
		}

		_maxColumn += columnOffset;
	}
}
