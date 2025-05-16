namespace AdventOfCode.Year2023.Day01.Puzzle;

internal interface ICalibrationRetriever
{
    public int RetrieveCalibrationValue(ReadOnlySpan<char> documentLine);
}
