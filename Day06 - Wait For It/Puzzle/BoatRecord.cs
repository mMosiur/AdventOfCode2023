using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Year2023.Day06.Puzzle;

[method: SetsRequiredMembers]
internal readonly record struct BoatRecord(int Time, int Distance)
{
	public required int Time { get; init; } = Time;
	public required int Distance { get; init; } = Distance;
}
