using System.Text.RegularExpressions;
using AdventOfCode.Common.SpanExtensions;

namespace AdventOfCode.Year2023.Day04.Puzzle;

internal sealed class InputReader
{
	private static readonly Regex ScratchcardRegex = new(@"^\s*Card +(\d+): ([\d ]+) \| ([\d ]+)\s*$", RegexOptions.Compiled);

	private static Scratchcard ReadFromRegexMatch(Match match)
	{
		int cardNumber = int.Parse(match.Groups[1].Value);
		List<int> winningNumbers = new(10);
		foreach (var split in match.Groups[2].ValueSpan.Split(' '))
		{
			if (split.IsEmpty) continue;
			winningNumbers.Add(int.Parse(split));
		}
		List<int> myNumbers = new(25);
		foreach (var split in match.Groups[3].ValueSpan.Split(' '))
		{
			if (split.IsWhiteSpace()) continue;
			myNumbers.Add(int.Parse(split));
		}
		return new Scratchcard
		{
			Id = cardNumber,
			WinningNumbers = winningNumbers,
			MyNumbers = myNumbers
		};
	}

	public IReadOnlyList<Scratchcard> ReadInput(IEnumerable<string> inputLines)
		=> inputLines
			.Select(l => ScratchcardRegex.Match(l))
			.Select(ReadFromRegexMatch)
			.ToArray();
}
