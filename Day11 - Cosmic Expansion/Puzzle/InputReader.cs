namespace AdventOfCode.Year2023.Day11.Puzzle;

internal sealed class InputReader
{
    private const int InitialGalaxyListSize = 500;
    private readonly char _galaxyChar;

    public InputReader(char galaxyChar)
    {
        _galaxyChar = galaxyChar;
    }

    public IReadOnlyCollection<Point> ReadGalaxyPositions(string input)
    {
        var list = new List<Point>(InitialGalaxyListSize);
        int row = 0;
        foreach (var lineSpan in input.AsSpan().EnumerateLines())
        {
            int column = 0;
            foreach (char c in lineSpan)
            {
                if (c == _galaxyChar)
                {
                    list.Add(new(row, column));
                }

                column++;
            }

            row++;
        }

        return list;
    }
}
