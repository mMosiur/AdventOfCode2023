using System.Text.RegularExpressions;

namespace AdventOfCode.Year2023.Day07.Puzzle;

internal sealed class InputReader
{
	private static readonly Regex HandPattern = new(@"^^([2-9TJQKA]{5}) (\d+)$", RegexOptions.Compiled);

	private readonly HandBuilder _handBuilder = new();

	public IReadOnlyCollection<Hand> Read(IEnumerable<string> inputLines)
	{
		return inputLines
			.Select(ReadHand)
			.ToArray();
	}

	private Hand ReadHand(string inputLine)
	{
		_handBuilder.Reset();

		var match = HandPattern.Match(inputLine);
		if (!match.Success)
		{
			throw new FormatException($"Invalid input line: {inputLine}");
		}

		string handRepresentation = match.Groups[1].Value;
		int bid = int.Parse(match.Groups[2].Value);

		return _handBuilder
			.WithCards(handRepresentation)
			.WithBid(bid)
			.Build();
	}
}
