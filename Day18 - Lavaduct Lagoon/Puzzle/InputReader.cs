using System.Text.RegularExpressions;
using AdventOfCode.Common.StringExtensions;

namespace AdventOfCode.Year2023.Day18.Puzzle;

internal static partial class InputReader
{
    public static DigPlan Read(string input)
    {
        return new(
            input.EnumerateLines()
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(ParseRow)
        );
    }

    [GeneratedRegex(@"^([UDLR]) (\d+) \((\#[0-9a-f]{6})\)$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
    private static partial Regex RowRegex { get; }

    private static DigPlanRow ParseRow(string row)
    {
        var match = RowRegex.Match(row);
        if (!match.Success)
        {
            throw new FormatException($"Invalid input row: '{row}'");
        }

        return new(
            Direction: DirectionHelpers.Parse(match.Groups[1].ValueSpan),
            Distance: int.Parse(match.Groups[2].ValueSpan),
            Color: Color.FromHex(match.Groups[3].ValueSpan)
        );
    }
}
