using System.Text.RegularExpressions;

namespace AdventOfCode.Year2023.Day07.Puzzle.Input;

internal static class InputReader
{
    private static readonly Regex HandPattern = new(@"^^([2-9TJQKA]{5}) (\d+)$", RegexOptions.Compiled);

    public static IReadOnlyCollection<InputRow> Read(IEnumerable<string> inputLines)
    {
        return inputLines
            .Select(ReadHand)
            .ToArray();
    }

    private static InputRow ReadHand(string inputLine)
    {
        var match = HandPattern.Match(inputLine);
        if (!match.Success)
        {
            throw new FormatException($"Invalid input line: {inputLine}");
        }

        string handRepresentation = match.Groups[1].Value;
        int bid = int.Parse(match.Groups[2].Value);

        return new()
        {
            HandRepresentation = handRepresentation,
            Bid = bid
        };
    }
}
