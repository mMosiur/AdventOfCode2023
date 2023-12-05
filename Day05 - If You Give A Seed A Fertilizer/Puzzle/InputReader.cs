using System.Text.RegularExpressions;
using AdventOfCode.Common.SpanExtensions;

namespace AdventOfCode.Year2023.Day05.Puzzle;

internal sealed class InputReader
{
	private static readonly string[] NewLines = { "\r\n", "\r", "\n" };
	private static readonly string[] DoubleNewLines = NewLines.Select(nl => $"{nl}{nl}").ToArray();
	private static readonly Regex SeedNumbersRegex = new(@"^seeds: ([\d ]+)$", RegexOptions.Compiled);
	private static readonly Regex NumberMapHeaderRegex = new(@"^(?<SourceNumberCategory>\w+)-to-(?<DestinationNumberCategory>\w+) map:$", RegexOptions.Compiled);

	private static IReadOnlyList<uint> ReadSeedNumbers(string input)
	{
		var match = SeedNumbersRegex.Match(input);
		if (!match.Success)
		{
			throw new InputException($"""Seed numbers line was in an unrecognized format ("{input}").""");
		}

		var seedsSpan = match.Groups[1].ValueSpan;
		List<uint> seedNumbers = new(seedsSpan.Count(' ') + 1);
		foreach (var seedNumberSpan in seedsSpan.Split(' '))
		{
			if (!uint.TryParse(seedNumberSpan, out uint seedNumber))
			{
				throw new InputException($"""Could not parse a number in a seed number line ("{seedNumberSpan}").""");
			}

			seedNumbers.Add(seedNumber);
		}

		return seedNumbers;
	}

	private static (NumberCategory SourceCategory, NumberCategory DestinationCategory) ReadNumberMapHeader(string header)
	{
		var match = NumberMapHeaderRegex.Match(header);
		if (!match.Success)
		{
			throw new InputException($"""Number map header was in an unrecognized format ("{header}").""");
		}

		return (
			SourceCategory: Enum.Parse<NumberCategory>(match.Groups["SourceNumberCategory"].ValueSpan, ignoreCase: true),
			DestinationCategory: Enum.Parse<NumberCategory>(match.Groups["DestinationNumberCategory"].ValueSpan, ignoreCase: true)
		);
	}

	private static NumberMapLine ReadNumberMapLine(ReadOnlySpan<char> lineSpan)
	{
		if (!lineSpan.TrySplitInThree(
			    separator: ' ',
			    out var firstNumberSpan,
			    out var secondNumberSpan,
			    out var thirdNumberSpan,
			    allowMultipleSeparators: true))
		{
			throw new InputException($"""Could not parse number map line from input line "{lineSpan}".""");
		}

		return new NumberMapLine(
			destinationRangeStart: uint.Parse(firstNumberSpan),
			sourceRangeStart: uint.Parse(secondNumberSpan),
			rangeLength: uint.Parse(thirdNumberSpan));
	}

	private static NumberMap ReadNumberMap(string section)
	{
		var sectionSpan = section.AsSpan();
		var lineSpanIterator = sectionSpan.EnumerateLines();
		if (!lineSpanIterator.MoveNext()) throw new InputException($"Could not read number map header from input section \"{section}\".");
		(NumberCategory sourceCategory, NumberCategory destinationCategory) = ReadNumberMapHeader(lineSpanIterator.Current.ToString());
		var numberMapLines = new List<NumberMapLine>(sectionSpan.Count('\n'));
		while (lineSpanIterator.MoveNext())
		{
			var numberMapLine = ReadNumberMapLine(lineSpanIterator.Current);
			numberMapLines.Add(numberMapLine);
		}

		return new NumberMap(sourceCategory, destinationCategory, numberMapLines);
	}

	public Almanac ReadInput(string input)
	{
		string[] result = input.Split(DoubleNewLines, StringSplitOptions.TrimEntries);
		string seedNumbersRow = result[0];
		var seedNumbers = ReadSeedNumbers(seedNumbersRow);
		var numberMaps = new List<NumberMap>(result.Length - 1);
		for (int i = 1; i < result.Length; i++)
		{
			string mapSectionLine = result[i];
			var mapSection = ReadNumberMap(mapSectionLine);
			numberMaps.Add(mapSection);
		}

		return new Almanac(seedNumbers, numberMaps);
	}
}
