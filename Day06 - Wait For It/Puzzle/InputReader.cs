using System.Text.RegularExpressions;
using AdventOfCode.Common.SpanExtensions;

namespace AdventOfCode.Year2023.Day06.Puzzle;

internal sealed class InputReader
{
	private static readonly Regex InputRegex = new(@"^\s*Time:\s*([\d ]+)\s*\r?\nDistance:\s*([\d ]+)\s*$");

	private const int DefaultCapacity = 4;

	public IReadOnlyList<BoatRecord> ReadRecords(string input)
	{
		var match = InputRegex.Match(input);
		if (!match.Success)
		{
			throw new FormatException("Input is not in the expected format.");
		}

		var timeValues = new List<int>(DefaultCapacity);
		foreach (var timeValueSpan in match.Groups[1].ValueSpan.Split(' '))
		{
			if (timeValueSpan.IsEmpty) continue;
			if (!int.TryParse(timeValueSpan, out int timeValue))
			{
				throw new FormatException($"Could not parse time value '{timeValueSpan}'.");
			}

			timeValues.Add(timeValue);
		}

		var distanceValues = new List<int>(timeValues.Count);
		foreach (var distanceValueSpan in match.Groups[2].ValueSpan.Split(' '))
		{
			if (distanceValueSpan.IsEmpty) continue;
			if (!int.TryParse(distanceValueSpan, out int distanceValue))
			{
				throw new FormatException($"Could not parse distance value '{distanceValueSpan}'.");
			}

			distanceValues.Add(distanceValue);
		}

		if (timeValues.Count != distanceValues.Count)
		{
			throw new FormatException("Numbers of time values and distance values do not match.");
		}

		return timeValues.Zip(distanceValues, (time, distance) => new BoatRecord(time, distance)).ToList();
	}
}
