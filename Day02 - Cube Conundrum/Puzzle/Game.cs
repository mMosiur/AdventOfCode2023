namespace AdventOfCode.Year2023.Day02.Puzzle;

internal readonly struct Game
{
    public required int Id { get; init; }
    public required IReadOnlyList<CubeSet> Cubes { get; init; }
}
