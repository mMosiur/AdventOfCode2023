using AdventOfCode.Common;
using AdventOfCode.Common.SpanExtensions;

namespace AdventOfCode.Year2023.Day09.Puzzle.Oasis;

internal sealed class ReportReader
{
    public Report ReadReport(IEnumerable<string> inputLines)
    {
        var valueHistories = inputLines.Select(l => ReadReportLine(l));
        return new Report(valueHistories);
    }

    private static ValueHistory ReadReportLine(ReadOnlySpan<char> line)
    {
        line = line.Trim();
        int valueCount = line.Count(' ') + 1;
        var values = new List<int>(valueCount);
        foreach (var numberSpan in line.SplitAsSpans(' '))
        {
            if (!int.TryParse(numberSpan, out int number))
            {
                throw new InputException($"Invalid number format '{numberSpan}'.");
            }

            values.Add(number);
        }

        return new ValueHistory(values);
    }
}
