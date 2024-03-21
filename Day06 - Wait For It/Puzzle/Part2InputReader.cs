using System.Text.RegularExpressions;

namespace AdventOfCode.Year2023.Day06.Puzzle;

internal sealed class Part2InputReader
{
	private static readonly Regex InputRegex = new(@"^\s*Time:\s*([\d ]+)\s*\r?\nDistance:\s*([\d ]+)\s*$");

	private static long GetNumberFromSpanIgnoringSpaces(ReadOnlySpan<char> span)
	{
		Span<char> joinedNumberSpan = stackalloc char[span.Length];
		int digitCount = 0;
		foreach (char c in span)
		{
			if (c is ' ') continue;
			if (!char.IsDigit(c))
			{
				throw new FormatException($"Unexpected character '{c}' in '{span}'.");
			}
			joinedNumberSpan[digitCount] = c;
			digitCount++;
		}
		return long.Parse(joinedNumberSpan[..digitCount]);
	}

	public BoatRecord ReadRecord(string input)
	{
		var match = InputRegex.Match(input);
		if (!match.Success)
		{
			throw new FormatException("Input is not in the expected format.");
		}

		var separatedTimeValuesSpan = match.Groups[1].ValueSpan;
		long timeValue = GetNumberFromSpanIgnoringSpaces(separatedTimeValuesSpan);

		var separatedDistanceValuesSpan = match.Groups[2].ValueSpan;
		long distanceValue = GetNumberFromSpanIgnoringSpaces(separatedDistanceValuesSpan);

		return new BoatRecord(timeValue, distanceValue);
	}
}
