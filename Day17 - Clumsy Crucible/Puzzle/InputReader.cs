using AdventOfCode.Common;

namespace AdventOfCode.Year2023.Day17.Puzzle;

internal static class InputReader
{
    public static HeatLossMap Read(IEnumerable<string> inputLines)
    {
        int[][] numbers = inputLines
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => l.Trim().Select(c => (int)char.GetNumericValue(c)).ToArray())
            .ToArray();

        int width = numbers[0].Length;
        int height = numbers.Length;
        int[,] values = new int[height, width];

        for (int x = 0; x < height; x++)
        {
            if (numbers[x].Length != width)
            {
                throw new InputException("All rows must have the same length.");
            }

            for (int y = 0; y < width; y++)
            {
                values[x, y] = numbers[x][y];
            }
        }

        return new(values);
    }
}
