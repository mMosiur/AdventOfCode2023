namespace AdventOfCode.Year2023.Day19.Puzzle;

internal sealed class RatingSystem
{
    private readonly Dictionary<string, Workflow> _workflows;
    private readonly string _startingWorkflowName;
    private readonly string _acceptWorkflowName;
    private readonly string _rejectWorkflowName;

    private RatingSystem(Dictionary<string, Workflow> workflows, string startingWorkflowName, string acceptWorkflowName, string rejectWorkflowName)
    {
        _workflows = workflows;
        _startingWorkflowName = startingWorkflowName;
        _acceptWorkflowName = acceptWorkflowName;
        _rejectWorkflowName = rejectWorkflowName;
    }

    public static RatingSystem BuildFromWorkflows(IEnumerable<Workflow> workflows, string startingWorkflowName, string acceptWorkflowName, string rejectWorkflowName)
    {
        var workflowDictionary = workflows.ToDictionary(w => w.Name);
        if (!workflowDictionary.ContainsKey(startingWorkflowName))
            throw new ArgumentException($"Starting workflow '{startingWorkflowName}' not found in provided workflows.", nameof(startingWorkflowName));
        workflowDictionary.Add(acceptWorkflowName, Workflow.FinalWorkflow(acceptWorkflowName));
        workflowDictionary.Add(rejectWorkflowName, Workflow.FinalWorkflow(rejectWorkflowName));

        return new(workflowDictionary, startingWorkflowName, acceptWorkflowName, rejectWorkflowName);
    }

    public bool IsPartAccepted(PartRatings partRatings)
    {
        var currentWorkflow = _workflows[_startingWorkflowName];
        while (currentWorkflow.Name != _acceptWorkflowName && currentWorkflow.Name != _rejectWorkflowName)
        {
            foreach (var rule in currentWorkflow.Rules)
            {
                var doesRuleMatch = IsMatch(rule, partRatings);
                if (!doesRuleMatch) continue; // try next rule
                // found matching rule, go to its destination
                currentWorkflow = _workflows[rule.Destination];
                break;
            }
        }

        if (currentWorkflow.Name == _acceptWorkflowName) return true;
        if (currentWorkflow.Name == _rejectWorkflowName) return false;
        throw new InvalidOperationException("Reached an unexpected workflow state.");
    }

    private static bool IsMatch(Rule rule, PartRatings partRatings)
    {
        if (rule is TerminatingRule) return true;
        var conditionalRule = (ConditionalRule)rule;
        var categoryValue = conditionalRule.Category switch
        {
            RatingCategory.X => partRatings.X,
            RatingCategory.M => partRatings.M,
            RatingCategory.A => partRatings.A,
            RatingCategory.S => partRatings.S,
            _ => throw new InvalidOperationException("Invalid rating category.")
        };
        return conditionalRule.Comparison switch
        {
            Operator.LessThan => categoryValue < conditionalRule.Value,
            Operator.GreaterThan => categoryValue > conditionalRule.Value,
            _ => throw new InvalidOperationException()
        };
    }
}
