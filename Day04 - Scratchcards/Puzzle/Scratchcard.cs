namespace AdventOfCode.Year2023.Day04.Puzzle;

internal readonly struct Scratchcard
{
	public required int Id { get; init; }
	public required IReadOnlyList<int> WinningNumbers { get; init; }
	public required IReadOnlyList<int> MyNumbers { get; init; }
}
