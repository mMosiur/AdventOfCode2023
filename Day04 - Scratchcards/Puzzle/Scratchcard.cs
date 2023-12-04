namespace AdventOfCode.Year2023.Day04.Puzzle;

internal sealed class Scratchcard
{
	public required int Id { get; init; }
	public required IReadOnlyList<int> WinningNumbers { get; init; }
	public required IReadOnlyList<int> MyNumbers { get; init; }

	public int Copies { get; set; } = 1;
}
