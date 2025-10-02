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
        var workflowDictionary = _puzzleInput.Workflows.ToDictionary(w => w.Name);
        workflowDictionary["R"] = Workflow.FinalWorkflow("R");
        workflowDictionary["A"] = Workflow.FinalWorkflow("A");

        var approvedParts = _puzzleInput.Parts.Where(p => IsApproved(workflowDictionary, p)).ToList();
        var result = approvedParts
            .Select(p => p.X + p.M + p.A + p.S)
            .Sum();

        return result.ToString();
    }

    private static bool IsApproved(IReadOnlyDictionary<string, Workflow> workflows, Part part)
    {
        var currentWorkflow = workflows["in"];
        while (currentWorkflow.Name is not "R" and not "A")
        {
            foreach (var rule in currentWorkflow.Rules)
            {
                var doesRuleMatch = IsMatch(rule, part);
                if (!doesRuleMatch) continue; // try next rule
                currentWorkflow = workflows[rule.Destination];
                break;
            }
        }

        return currentWorkflow.Name switch
        {
            "A" => true,
            "R" => false,
            _ => throw new InvalidOperationException()
        };
    }

    private static bool IsMatch(Rule rule, Part part)
    {
        if (rule is TerminatingRule) return true;
        var conditionalRule = (ConditionalRule)rule;
        var categoryValue = conditionalRule.Category switch
        {
            Category.X => part.X,
            Category.M => part.M,
            Category.A => part.A,
            Category.S => part.S,
            _ => throw new InvalidOperationException()
        };
        return conditionalRule.Comparison switch
        {
            Operator.LessThan => categoryValue < conditionalRule.Value,
            Operator.GreateThan => categoryValue > conditionalRule.Value,
            _ => throw new InvalidOperationException()
        };
    }

    public override string SolvePart2()
    {
        return "UNSOLVED";
    }
}
