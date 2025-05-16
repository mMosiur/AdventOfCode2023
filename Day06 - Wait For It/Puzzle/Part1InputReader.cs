using System.Text.RegularExpressions;
using AdventOfCode.Common.SpanExtensions;

namespace AdventOfCode.Year2023.Day06.Puzzle;

internal sealed class Part1InputReader
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

        var timeValues = GetListOfNumbersInSpan(match.Groups[1].ValueSpan);
        var distanceValues = GetListOfNumbersInSpan(match.Groups[2].ValueSpan);

        if (timeValues.Count != distanceValues.Count)
        {
            throw new FormatException("Numbers of time values and distance values do not match.");
        }

        return timeValues.Zip(distanceValues, (time, distance) => new BoatRecord(time, distance)).ToList();
    }

    private List<int> GetListOfNumbersInSpan(ReadOnlySpan<char> span)
    {
        var numbers = new List<int>(DefaultCapacity);
        foreach (var numberSpan in span.SplitAsSpans(' '))
        {
            if (numberSpan.IsEmpty) continue;
            if (!int.TryParse(numberSpan, out int number))
            {
                throw new FormatException($"Could not parse number '{numberSpan}'.");
            }

            numbers.Add(number);
        }

        return numbers;
    }
}
