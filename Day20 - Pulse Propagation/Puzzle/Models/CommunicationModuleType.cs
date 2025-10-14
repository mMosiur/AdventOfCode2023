namespace AdventOfCode.Year2023.Day20.Puzzle.Models;

internal enum CommunicationModuleType
{
    Broadcast = 0, // no prefix
    FlipFlop = 1, // % prefix
    Conjunction = 2, // & prefix
}

internal static class CommunicationModuleTypeHelper
{
    public static CommunicationModuleType FromPrefix(ReadOnlySpan<char> prefix) => prefix.Trim() switch
    {
        "%" => CommunicationModuleType.FlipFlop,
        "&" => CommunicationModuleType.Conjunction,
        "" => CommunicationModuleType.Broadcast,
        _ => throw new ArgumentOutOfRangeException(nameof(prefix), $"Unknown communication module prefix '{prefix.ToString()}'"),
    };
}
