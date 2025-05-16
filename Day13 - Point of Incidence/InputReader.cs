using AdventOfCode.Common;
using AdventOfCode.Common.SpanExtensions;

namespace AdventOfCode.Year2023.Day13;

internal static class InputReader
{
    public static IReadOnlyList<Mirror> ReadInput(string input)
    {
        int mirrorIndex = 0;
        var mirrors = new List<Mirror>(2);
        foreach (var mirrorSpan in input.AsSpan().Trim().SplitAsSpans("\n\n"))
        {
            int mirrorWidth = mirrorSpan.IndexOf('\n');
            int mirrorHeight = mirrorSpan.Count('\n') + 1;
            var mirrorTerrain = new Terrain[mirrorHeight, mirrorWidth];

            int y = 0;
            foreach (var mirrorLine in mirrorSpan.SplitAsSpans('\n'))
            {
                if (mirrorLine.Length != mirrorWidth)
                {
                    throw new InputException($"Uneven mirror width at mirror index {mirrorIndex} (expected {mirrorWidth}, got {mirrorLine.Length})");
                }

                for (int x = 0; x < mirrorWidth; x++)
                {
                    mirrorTerrain[y, x] = ParseTerrain(mirrorLine[x]);
                }

                y++;
            }

            mirrors.Add(new(mirrorTerrain));
            mirrorIndex++;
        }

        return mirrors;
    }

    private static Terrain ParseTerrain(char c) => c switch
    {
        '.' => Terrain.Ash,
        '#' => Terrain.Rocks,
        _ => throw new InputException($"Invalid terrain character: '{c}'")
    };
}
