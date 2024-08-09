namespace AdventOfCode.Year2023.Day14;

internal static class InputReader
{
	public static SpaceType[,] Read(IEnumerable<string> inputLines)
	{
		var sourceList = inputLines.Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
		int rowCount = sourceList.Count;
		if (rowCount == 0)
		{
			throw new InputException("Input was empty");
		}
		int columnCount = sourceList[0].Length;
		if (sourceList.Any(l => l.Length != columnCount))
		{
			throw new InputException("Input was not in a rectangular shape");
		}

		var plane = new SpaceType[rowCount, columnCount];
		for (int row = 0; row < rowCount; row++)
		{
			for (int col = 0; col < columnCount; col++)
			{
				plane[row, col] = ParseCharType(sourceList[row][col]);
			}
		}

		return plane;
	}

	private static SpaceType ParseCharType(char c)
		=> c switch
		{
			'.' => SpaceType.Empty,
			'#' => SpaceType.CubeRock,
			'O' => SpaceType.RoundRock,
			_ => throw new InputException($"Invalid space type '{c}'")
		};
}
