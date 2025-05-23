namespace AdventOfCode.Year2023.Day02.Puzzle;

internal readonly struct CubeSet
{
    public int Red { get; init; }
    public int Green { get; init; }
    public int Blue { get; init; }

    public int Power => Red * Green * Blue;
}
