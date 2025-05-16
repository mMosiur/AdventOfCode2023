using AdventOfCode.Common;

namespace AdventOfCode.Year2023.Day13;

internal sealed class MirrorAnalyzer
{
	private readonly int _rowSummaryMultiplier;
	private readonly int _columnSummaryMultiplier;

	public MirrorAnalyzer(int rowSummaryMultiplier, int columnSummaryMultiplier)
	{
		_rowSummaryMultiplier = rowSummaryMultiplier;
		_columnSummaryMultiplier = columnSummaryMultiplier;
	}

	public int SummarizeMirrorNote(Mirror mirror)
	{
		foreach (int row in FindPotentialHorizontalReflectionLines(mirror))
		{
			if (CheckHorizontalReflection(mirror, row))
			{
				// Return early as each mirror can only have one reflection
				return _rowSummaryMultiplier * row;
			}
		}

		foreach (int col in FindPotentialVerticalReflections(mirror))
		{
			if (CheckVerticalReflection(mirror, col))
			{
				// Return early as each mirror can only have one reflection
				return _columnSummaryMultiplier * col;
			}
		}

		throw new DaySolverException("No reflection found");
	}

	private static IEnumerable<int> FindPotentialHorizontalReflectionLines(Mirror mirror)
	{
		int prevRowHash = mirror.GetRowHash(0);
		for (int row = 1; row < mirror.RowCount; row++)
		{
			int rowHash = mirror.GetRowHash(row);
			if (prevRowHash == rowHash)
			{
				yield return row;
			}

			prevRowHash = rowHash;
		}
	}

	private static bool CheckHorizontalReflection(Mirror mirror, int row)
	{
		int reflectedRow = row - 1;
		while (reflectedRow >= 0 && row < mirror.RowCount)
		{
			int reflectedRowHash = mirror.GetRowHash(reflectedRow);
			int rowHash = mirror.GetRowHash(row);
			if (rowHash != reflectedRowHash)
			{
				return false;
			}

			reflectedRow--;
			row++;
		}

		return true;
	}

	private static IEnumerable<int> FindPotentialVerticalReflections(Mirror mirror)
	{
		int prevColHash = mirror.GetColumnHash(0);
		for (int col = 1; col < mirror.ColumnCount; col++)
		{
			int colHash = mirror.GetColumnHash(col);
			if (prevColHash == colHash)
			{
				yield return col;
			}

			prevColHash = colHash;
		}
	}

	private static bool CheckVerticalReflection(Mirror mirror, int col)
	{
		int reflectedCol = col - 1;
		while (reflectedCol >= 0 && col < mirror.ColumnCount)
		{
			int reflectedColHash = mirror.GetColumnHash(reflectedCol);
			int colHash = mirror.GetColumnHash(col);
			if (colHash != reflectedColHash)
			{
				return false;
			}

			reflectedCol--;
			col++;
		}

		return true;
	}
}
