namespace AdventOfCode.Year2023.Day05.Puzzle;

internal readonly record struct NumberMapLine
{
    public Range SourceRange { get; }
    public long DestinationOffset { get; }

    public NumberMapLine(uint destinationRangeStart, uint sourceRangeStart, uint rangeLength)
    {
        SourceRange = new(sourceRangeStart, sourceRangeStart + rangeLength - 1);
        DestinationOffset = (long)destinationRangeStart - sourceRangeStart;
    }
}
