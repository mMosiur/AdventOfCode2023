namespace AdventOfCode.Year2023.Day01.Puzzle;

internal sealed class SpellingDigitCalibrationRetriever : ICalibrationRetriever
{
	private static readonly char[] Digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
	private static readonly string[] DigitSpellings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

	public int RetrieveCalibrationValue(ReadOnlySpan<char> documentLine)
	{
		Span<int> firstIndexes = stackalloc int[10];
		Span<int> lastIndexes = stackalloc int[10];

		for (int digit = 0; digit < 10; digit++)
		{
			char digitChar = Digits[digit];
			string digitSpelling = DigitSpellings[digit];

			(int firstSpellingIndex, int lastSpellingIndex) = FindFirstAndLastOccurrences(documentLine, digitSpelling);
			(int firstDigitIndex, int lastDigitIndex) = FindFirstAndLastOccurrences(documentLine, digitChar);
			firstIndexes[digit] = (firstSpellingIndex, firstDigitIndex) switch
			{
				(-1, -1) => -1,
				(-1, _) => firstDigitIndex,
				(_, -1) => firstSpellingIndex,
				(_, _) => Math.Min(firstSpellingIndex, firstDigitIndex)
			};
			lastIndexes[digit] = (lastSpellingIndex, lastDigitIndex) switch
			{
				(-1, -1) => -1,
				(-1, _) => lastDigitIndex,
				(_, -1) => lastSpellingIndex,
				(_, _) => Math.Max(lastSpellingIndex, lastDigitIndex)
			};
		}

		int firstDigit = firstIndexes.GetIndexOfLowestValueInRange(0, 9);
		if (firstDigit < 0) throw new DaySolverException($"Document line '{documentLine}' does not contain any digits nor spellings.");

		int lastDigit = lastIndexes.GetIndexOfHighestValueInRange(0, 9);
		if (lastDigit < 0) throw new DaySolverException($"Document line '{documentLine}' does not contain any digits nor spellings.");

		return firstDigit * 10 + lastDigit;
	}

	private static (int, int) FindFirstAndLastOccurrences(ReadOnlySpan<char> documentLine, ReadOnlySpan<char> sequence)
		=> (documentLine.IndexOf(sequence), documentLine.LastIndexOf(sequence));

	private static (int, int) FindFirstAndLastOccurrences(ReadOnlySpan<char> documentLine, char character)
		=> (documentLine.IndexOf(character), documentLine.LastIndexOf(character));
}
