using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Year2023.Day06.Puzzle;

[method: SetsRequiredMembers]
internal readonly record struct BoatRecord(long Time, long Distance)
{
    public required long Time { get; init; } = Time;
    public required long Distance { get; init; } = Distance;
}
