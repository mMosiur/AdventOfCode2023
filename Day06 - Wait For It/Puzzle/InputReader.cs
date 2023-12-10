using System.Text.RegularExpressions;
using AdventOfCode.Common.SpanExtensions;

namespace AdventOfCode.Year2023.Day06.Puzzle;

internal sealed class InputReader
{
	private static readonly Regex InputRegex = new(@"^\s*Time:\s*([\d ]+)\s*\r?\nDistance:\s*([\d ]+)\s*$");

	public IReadOnlyList<BoatRecord> ReadRecords(string input)
	{
		var match = InputRegex.Match(input);
		if (!match.Success)
		{
			throw new FormatException("Input is not in the expected format.");
		}

		var times = new List<int>(4);
		foreach (var timeSpan in match.Groups[1].ValueSpan.Split(' '))
		{
			if (timeSpan.IsEmpty) continue;
			if (!int.TryParse(timeSpan, out int time))
			{
				throw new FormatException($"Could not parse time number '{timeSpan}'.");
			}

			times.Add(time);
		}

		var distances = new List<int>(times.Count);
		foreach (var distanceSpan in match.Groups[2].ValueSpan.Split(' '))
		{
			if (distanceSpan.IsEmpty) continue;
			if (!int.TryParse(distanceSpan, out int distance))
			{
				throw new FormatException($"Could not parse distance number '{distanceSpan}'.");
			}

			distances.Add(distance);
		}

		if (times.Count != distances.Count)
		{
			throw new FormatException("Number of times and distances does not match.");
		}

		return Enumerable.Range(0, times.Count)
			.Select(i => new BoatRecord
			{
				Time = times[i],
				Distance = distances[i],
			}).ToList();
	}
}

internal readonly struct BoatRecord
{
	public required int Time { get; init; }
	public required int Distance { get; init; }
}
