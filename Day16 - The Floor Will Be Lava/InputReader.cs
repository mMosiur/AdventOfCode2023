using AdventOfCode.Common;
using AdventOfCode.Year2023.Day16.Puzzle.Cave;

namespace AdventOfCode.Year2023.Day16;

internal static class InputReader
{
    public static CaveTile[,] ReadCaveSpaceGrid(IEnumerable<string> inputLines)
    {
        string[] lines = inputLines.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
        int height = lines.Length;
        if (height == 0)
        {
            throw new InputException("No lines found in input.");
        }

        int width = lines[0].Length;
        if (lines.Any(l => l.Length != width))
        {
            throw new InputException("All lines must have the same length.");
        }

        var grid = new CaveTile[height, width];

        for (int row = 0; row < height; row++)
        {
            string line = lines[row];
            for (int col = 0; col < width; col++)
            {
                char c = line[col];
                grid[row, col] = c switch
                {
                    '.' => CaveTile.Empty,
                    '/' => CaveTile.RisingMirror,
                    '\\' => CaveTile.FallingMirror,
                    '-' => CaveTile.HorizontalSplitter,
                    '|' => CaveTile.VerticalSplitter,
                    _ => throw new InputException($"Invalid character '{c}' at ({col}, {row})."),
                };
            }
        }

        return grid;
    }
}
