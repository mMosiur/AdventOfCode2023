namespace AdventOfCode.Year2023.Day01.Puzzle;

internal sealed class SpellingDigitCalibrationRetriever : ICalibrationRetriever
{
	private static readonly char[] Digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
	private static readonly string[] DigitSpellings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

	public int RetrieveCalibrationValue(ReadOnlySpan<char> documentLine)
	{
		Span<int> firstIndexes = stackalloc int[10];
		Span<int> lastIndexes = stackalloc int[10];

		for (int i = 0; i < 10; i++)
		{
			char digit = Digits[i];
			string digitSpelling = DigitSpellings[i];

			(int firstSpellingIndex, int lastSpellingIndex) = FindFirstAndLastOccurrences(documentLine, digitSpelling);
			int firstDigitIndex = documentLine.IndexOf(digit);
			int lastDigitIndex = documentLine.LastIndexOf(digit);
			if(firstSpellingIndex < 0) firstSpellingIndex = int.MaxValue;
			if (lastSpellingIndex < 0) lastSpellingIndex = int.MinValue;
			if (firstDigitIndex < 0) firstDigitIndex = int.MaxValue;
			if (lastDigitIndex < 0) lastDigitIndex = int.MinValue;
			firstIndexes[i] = Math.Min(firstSpellingIndex, firstDigitIndex);
			lastIndexes[i] = Math.Max(lastSpellingIndex, lastDigitIndex);
		}

		(int firstIndex, int firstDigit) = GetLowestIndex(firstIndexes);
		(int lastIndex, int lastDigit) = GetHighestIndex(lastIndexes);

		return firstDigit * 10 + lastDigit;
	}

	private static (int Index, int Digit) GetLowestIndex(ReadOnlySpan<int> indexes)
	{
		int lowestIndex = int.MaxValue;
		int lowestDigit = -1;
		for (int i = 0; i < indexes.Length; i++)
		{
			if (indexes[i] == -1) continue;
			int index = indexes[i];
			if (index < lowestIndex)
			{
				lowestIndex = index;
				lowestDigit = i;
			}
		}

		if (lowestDigit == -1)
		{
			throw new DaySolverException("Document line does not contain any digits.");
		}

		return (lowestIndex, lowestDigit);
	}

	private static (int Index, int Digit) GetHighestIndex(ReadOnlySpan<int> indexes)
	{
		int highestIndex = int.MinValue;
		int highestDigit = -1;
		for (int i = 0; i < indexes.Length; i++)
		{
			if (indexes[i] == -1) continue;
			int index = indexes[i];
			if (index > highestIndex)
			{
				highestIndex = index;
				highestDigit = i;
			}
		}

		if (highestDigit == -1)
		{
			throw new DaySolverException("Document line does not contain any digits.");
		}

		return (highestIndex, highestDigit);
	}

	private static (int, int) FindFirstAndLastOccurrences(ReadOnlySpan<char> documentLine, ReadOnlySpan<char> sequence)
		=> (documentLine.IndexOf(sequence), documentLine.LastIndexOf(sequence));
}
