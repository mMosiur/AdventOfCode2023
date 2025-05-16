using AdventOfCode.Common;

namespace AdventOfCode.Year2023.Day13;

internal sealed class SmudgedMirrorAnalyzer
{
	private readonly int _rowSummaryMultiplier;
	private readonly int _columnSummaryMultiplier;

	public SmudgedMirrorAnalyzer(int rowSummaryMultiplier, int columnSummaryMultiplier)
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
			int hashDiff = rowHash ^ prevRowHash;
			if (BitMath.IsAtMostOneBitSet(hashDiff))
			{
				yield return row;
			}

			prevRowHash = rowHash;
		}
	}

	private static bool CheckHorizontalReflection(Mirror mirror, int reflectionRow)
	{
		bool smudgeFixed = false;
		int reflectedRow = reflectionRow - 1;
		while (reflectedRow >= 0 && reflectionRow < mirror.RowCount)
		{
			int rowHash = mirror.GetRowHash(reflectionRow);
			int reflectedRowHash = mirror.GetRowHash(reflectedRow);
			int hashDiff = rowHash ^ reflectedRowHash;
			if (hashDiff != 0)
			{
				// Hashes differ, check if only one bit is different
				if (!BitMath.IsAtMostOneBitSet(hashDiff))
				{
					// More than one bit is different, no reflection even
					return false;
				}

				if (smudgeFixed)
				{
					// Another smudge was already fixed
					return false;
				}

				// Fix this single smudge and carry on
				smudgeFixed = true;
			}

			reflectedRow--;
			reflectionRow++;
		}

		// We want to make sure that at least one smudge was fixed
		return smudgeFixed;
	}

	private static IEnumerable<int> FindPotentialVerticalReflections(Mirror mirror)
	{
		int prevColHash = mirror.GetColumnHash(0);
		for (int col = 1; col < mirror.ColumnCount; col++)
		{
			int colHash = mirror.GetColumnHash(col);
			int hashDiff = colHash ^ prevColHash;
			if (BitMath.IsAtMostOneBitSet(hashDiff))
			{
				yield return col;
			}

			prevColHash = colHash;
		}
	}

	private static bool CheckVerticalReflection(Mirror mirror, int reflectionCol)
	{
		bool smudgeFixed = false;
		int reflectedCol = reflectionCol - 1;
		while (reflectedCol >= 0 && reflectionCol < mirror.ColumnCount)
		{
			int reflectedColHash = mirror.GetColumnHash(reflectedCol);
			int colHash = mirror.GetColumnHash(reflectionCol);
			int hashDiff = colHash ^ reflectedColHash;
			if (hashDiff != 0)
			{
				// Hashes differ, check if only one bit is different
				if (!BitMath.IsAtMostOneBitSet(hashDiff))
				{
					// More than one bit is different, no reflection even
					return false;
				}

				if (smudgeFixed)
				{
					// Another smudge was already fixed
					return false;
				}

				// Fix this single smudge and carry on
				smudgeFixed = true;
			}

			reflectedCol--;
			reflectionCol++;
		}

		// We want to make sure that at least one smudge was fixed
		return smudgeFixed;
	}
}
