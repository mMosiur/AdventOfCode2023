using System.Buffers;

namespace AdventOfCode.Year2023.Day01.Puzzle;

internal sealed class CalibrationRetriever
{
	private static readonly SearchValues<char> Digits = SearchValues.Create(new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });

	public IReadOnlyCollection<string> DocumentLines { get; }

	public CalibrationRetriever(IEnumerable<string> documentLines)
	{
		DocumentLines = documentLines.ToArray();
	}

	public int SumAllCalibrationValues()
		=> DocumentLines
			.Select(l => RetrieveCalibrationValue(l))
			.Sum();

	public int RetrieveCalibrationValue(ReadOnlySpan<char> documentLine)
	{
		int firstDigitIndex = RetrieveFirstDigitIndex(documentLine);
		int lastDigitIndex = RetrieveLastDigitIndex(documentLine);

		if (firstDigitIndex == -1 || lastDigitIndex == -1)
		{
			throw new DaySolverException($"Document line '{documentLine}' does not contain at least one digit.");
		}

		int firstDigit = (int)char.GetNumericValue(documentLine[firstDigitIndex]);
		int lastDigit = (int)char.GetNumericValue(documentLine[lastDigitIndex]);

		return firstDigit * 10 + lastDigit;
	}

	private static int RetrieveFirstDigitIndex(ReadOnlySpan<char> documentLine) => documentLine.IndexOfAny(Digits);

	private static int RetrieveLastDigitIndex(ReadOnlySpan<char> documentLine) => documentLine.LastIndexOfAny(Digits);
}
