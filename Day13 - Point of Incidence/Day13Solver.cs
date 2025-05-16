using AdventOfCode.Common;

namespace AdventOfCode.Year2023.Day13;

public sealed class Day13Solver : DaySolver<Day13SolverOptions>
{
    public override int Year => 2023;
    public override int Day => 13;
    public override string Title => "Point of Incidence";

    private readonly Day13SolverOptions _options;
    private readonly IReadOnlyList<Mirror> _mirrors;

    public Day13Solver(Day13SolverOptions options) : base(options)
    {
        _options = options;
        _mirrors = InputReader.ReadInput(Input);
    }

    public Day13Solver(Action<Day13SolverOptions> configure)
        : this(DaySolverOptions.FromConfigureAction(configure)) { }

    public Day13Solver() : this(new Day13SolverOptions()) { }

    public override string SolvePart1()
    {
        var analyzer = new MirrorAnalyzer(_options.RowSummaryMultiplier, _options.ColumnSummaryMultiplier);
        int summary = _mirrors.Sum(analyzer.SummarizeMirrorNote);
        return summary.ToString();
    }

    public override string SolvePart2()
    {
        var analyzer = new SmudgedMirrorAnalyzer(_options.RowSummaryMultiplier, _options.ColumnSummaryMultiplier);
        int summary = _mirrors.Sum(analyzer.SummarizeMirrorNote);
        return summary.ToString();
    }
}
