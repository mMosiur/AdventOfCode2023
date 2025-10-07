using AdventOfCode.Common;
using AdventOfCode.Year2023.Day19.Puzzle;

namespace AdventOfCode.Year2023.Day19;

public sealed class Day19Solver : DaySolver<Day19SolverOptions>
{
    public override int Year => 2023;
    public override int Day => 19;
    public override string Title => "Aplenty";

    private readonly PuzzleInput _puzzleInput;
    private readonly RatingSystem _ratingSystem;

    public Day19Solver(Day19SolverOptions options) : base(options)
    {
        _puzzleInput = InputReader.Read(Input);
        _ratingSystem = RatingSystem.BuildFromWorkflows(
            _puzzleInput.Workflows,
            startingWorkflowName: Options.StartingWorkflowName,
            acceptWorkflowName: Options.AcceptWorkflowName,
            rejectWorkflowName: Options.RejectWorkflowName);
    }

    public Day19Solver(Action<Day19SolverOptions> configure) : this(DaySolverOptions.FromConfigureAction(configure)) { }

    public Day19Solver() : this(new Day19SolverOptions()) { }

    public override string SolvePart1()
    {
        return _puzzleInput.Parts
            .Where(_ratingSystem.IsPartAccepted)
            .Select(p => p.X + p.M + p.A + p.S)
            .Sum()
            .ToString();
    }

    public override string SolvePart2()
    {
        var analyzer = new RatingSystemAnalyzer(_ratingSystem);
        var fullRangeCombinations = RatingSystemCombinations.FullRange(Options.MinimumRatingValue, Options.MaximumRatingValue);
        var result = analyzer.AnalyzeCombinations(fullRangeCombinations);
        var acceptCombinationsCount = result.CountAcceptCombinations();
        return acceptCombinationsCount.ToString();
    }
}
