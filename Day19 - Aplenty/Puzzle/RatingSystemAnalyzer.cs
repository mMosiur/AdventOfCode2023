namespace AdventOfCode.Year2023.Day19.Puzzle;

internal sealed class RatingSystemAnalyzer(RatingSystem ratingSystem)
{
    private readonly RatingSystem _ratingSystem = ratingSystem;

    public RatingSystemAnalysisResult AnalyzeCombinations(RatingSystemCombinations allCombinations)
    {
        var initialCombinations = allCombinations.Clone();
        var startingWorkflow = _ratingSystem.Workflows[_ratingSystem.StartingWorkflowName];
        var acceptCombinations = new List<RatingSystemCombinations>();
        var rejectCombinations = new List<RatingSystemCombinations>();
        PassCombinationsThroughWorkflow(initialCombinations, startingWorkflow, in acceptCombinations, in rejectCombinations);
        return new(acceptCombinations, rejectCombinations);
    }

    private void PassCombinationsThroughWorkflow(RatingSystemCombinations combinations, Workflow workflow, in List<RatingSystemCombinations> acceptCombinations, in List<RatingSystemCombinations> rejectCombinations)
    {
        if (workflow.Name == _ratingSystem.AcceptWorkflowName)
        {
            acceptCombinations.Add(combinations);
            return;
        }

        if (workflow.Name == _ratingSystem.RejectWorkflowName)
        {
            rejectCombinations.Add(combinations);
            return;
        }

        foreach (Rule rule in workflow.Rules)
        {
            var matchedCombinations = RetrieveMatchingRule(combinations, rule);
            var destinationWorkflow = _ratingSystem.Workflows[rule.Destination];
            PassCombinationsThroughWorkflow(matchedCombinations, destinationWorkflow, in acceptCombinations, in rejectCombinations);
        }
    }

    private static RatingSystemCombinations RetrieveMatchingRule(in RatingSystemCombinations combinations, Rule rule) =>
        rule switch
        {
            ConditionalRule conditionalRule => RetrieveMatchingRule(combinations, conditionalRule),
            TerminatingRule terminatingRule => combinations,
            _ => throw new InvalidOperationException("Unknown rule type.")
        };

    private static RatingSystemCombinations RetrieveMatchingRule(in RatingSystemCombinations combinations, ConditionalRule rule)
    {
        var categoryRange = combinations.GetCategoryRange(rule.Category);
        int end = rule.Value - 1;
        var (matchingRange, unmatchedRange) = rule.Comparison switch
        {
            Operator.LessThan => (Range.FromStartEnd(categoryRange.Start, end), Range.FromStartEnd(rule.Value, categoryRange.End)),
            Operator.GreaterThan => (Range.FromStartEnd(rule.Value + 1, categoryRange.End), Range.FromStartEnd(categoryRange.Start, rule.Value)),
            _ => throw new InvalidOperationException("Invalid comparison operator."),
        };
        combinations.SetCategoryRange(rule.Category, unmatchedRange);

        return combinations.WithCategoryRange(rule.Category, matchingRange);
    }
}

internal sealed record RatingSystemAnalysisResult(
    List<RatingSystemCombinations> AcceptCombinations,
    List<RatingSystemCombinations> RejectCombinations)
{
    public long CountAcceptCombinations()
    {
        return AcceptCombinations.Sum(c => c.Count);
    }

    public long CountRejectCombinations()
    {
        return RejectCombinations.Sum(c => c.Count);
    }
}
