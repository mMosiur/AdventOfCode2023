using AdventOfCode.Common;
using AdventOfCode.Year2023.Day19.Puzzle;

namespace AdventOfCode.Year2023.Day19;

public sealed class Day19Solver : DaySolver<Day19SolverOptions>
{
    public override int Year => 2023;
    public override int Day => 19;
    public override string Title => "Aplenty";

    private readonly PuzzleInput _puzzleInput;

    public Day19Solver(Day19SolverOptions options) : base(options)
    {
        _puzzleInput = InputReader.Read(Input);
    }

    public Day19Solver(Action<Day19SolverOptions> configure) : this(DaySolverOptions.FromConfigureAction(configure)) { }

    public Day19Solver() : this(new Day19SolverOptions()) { }

    public override string SolvePart1()
    {
        var ratingSystem = RatingSystem.BuildFromWorkflows(
            _puzzleInput.Workflows,
            startingWorkflowName: Options.StartingWorkflowName,
            acceptWorkflowName: Options.AcceptWorkflowName,
            rejectWorkflowName: Options.RejectWorkflowName);

        var result = _puzzleInput.Parts
            .Where(ratingSystem.IsPartAccepted)
            .Select(p => p.X + p.M + p.A + p.S)
            .Sum();

        return result.ToString();
    }

    public override string SolvePart2()
    {
        return "UNSOLVED";
    }
}
