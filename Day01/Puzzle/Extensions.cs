﻿namespace AdventOfCode.Year2023.Day01.Puzzle;

internal static class Extensions
{
	public static int GetIndexOfLowestValueInRange(this Span<int> span, int lowerBound, int upperBound)
	{
		int indexOfLowestValue = -1;
		int lowestValue = int.MaxValue;
		for (int i = 0; i < span.Length; i++)
		{
			int value = span[i];
			if (value < lowerBound || value > upperBound) continue;
			if (value >= lowestValue) continue;
			lowestValue = value;
			indexOfLowestValue = i;
		}

		return indexOfLowestValue;
	}

	public static int GetIndexOfHighestValueInRange(this Span<int> span, int lowerBound, int upperBound)
	{
		int indexOfHighestValue = -1;
		int highestValue = int.MinValue;
		for (int i = 0; i < span.Length; i++)
		{
			int value = span[i];
			if (value < lowerBound || value > upperBound) continue;
			if (value <= highestValue) continue;
			highestValue = value;
			indexOfHighestValue = i;
		}

		return indexOfHighestValue;
	}
}
