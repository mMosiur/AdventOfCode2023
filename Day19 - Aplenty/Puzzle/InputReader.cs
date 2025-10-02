using System.Text.RegularExpressions;
using AdventOfCode.Common;
using AdventOfCode.Common.EnumeratorExtensions;
using AdventOfCode.Common.StringExtensions;

namespace AdventOfCode.Year2023.Day19.Puzzle;

internal static partial class InputReader
{
    [GeneratedRegex(@"^(\w+)\{(.+)\}$")]
    private static partial Regex WorkflowRegex();

    [GeneratedRegex(@"^(?>(?'category'[xmas])(?'operator'[\<\>])(?'value'\d+)\:(?'destination'\w+),?)+(?'final'\w+)$")]
    private static partial Regex RuleRegex();

    [GeneratedRegex(@"^\{x=(?'x'\d+),m=(?'m'\d+),a=(?'a'\d+),s=(?'s'\d+)\}$")]
    private static partial Regex PartRegex();

    public static PuzzleInput Read(string input)
    {
        try
        {
            return InternalRead(input);
        }
        catch (Exception ex)
        {
            throw new InputException("Failed to parse puzzle input.", ex);
        }
    }

    private static PuzzleInput InternalRead(string input)
    {
        using var it = input.EnumerateLines().GetEnumerator();
        it.EnsureMoveToNextNonEmptyLine("Expected non-empty line for number of workflows.");

        var workflows = new List<Workflow>();
        while (!string.IsNullOrWhiteSpace(it.Current))
        {
            workflows.Add(ParseWorkflow(it.Current));
            it.EnsureMoveNext();
        }

        it.EnsureMoveToNextNonEmptyLine();

        var parts = new List<PartRatings>();
        while (!string.IsNullOrWhiteSpace(it.Current))
        {
            parts.Add(ParsePart(it.Current));

            if (!it.MoveNext()) break;
        }

        return new()
        {
            Workflows = workflows,
            Parts = parts,
        };
    }

    private static Workflow ParseWorkflow(string s)
    {
        var match = WorkflowRegex().Match(s);
        if (!match.Success) throw new FormatException($"Invalid workflow format: {s}");

        return new()
        {
            Name = match.Groups[1].Value,
            Rules = ParseRules(match.Groups[2].Value),
        };
    }

    private static Rule[] ParseRules(string s)
    {
        var match = RuleRegex().Match(s);
        if (!match.Success) throw new FormatException($"Invalid rule list format: {s}");

        int conditionalRuleCount = match.Groups["category"].Captures.Count;
        var rules = new Rule[conditionalRuleCount + 1];
        for (int i = 0; i < conditionalRuleCount; i++)
        {
            rules[i] = new ConditionalRule
            {
                Category = ParseCategory(match.Groups["category"].Captures[i].ValueSpan),
                Comparison = ParseOperator(match.Groups["operator"].Captures[i].ValueSpan),
                Value = int.Parse(match.Groups["value"].Captures[i].ValueSpan),
                Destination = match.Groups["destination"].Captures[i].Value
            };
        }

        rules[^1] = new TerminatingRule
        {
            Destination = match.Groups["final"].Value,
        };

        return rules;
    }

    private static RatingCategory ParseCategory(ReadOnlySpan<char> s)
        => s switch
        {
            "x" => RatingCategory.X,
            "m" => RatingCategory.M,
            "a" => RatingCategory.A,
            "s" => RatingCategory.S,
            _ => throw new ArgumentException($"Invalid category '{s}'.", nameof(s))
        };

    private static Operator ParseOperator(ReadOnlySpan<char> s)
        => s switch
        {
            "<" => Operator.LessThan,
            ">" => Operator.GreateThan,
            _ => throw new ArgumentException($"Invalid operator '{s}'.", nameof(s))
        };

    private static PartRatings ParsePart(string s)
    {
        var match = PartRegex().Match(s);
        if (!match.Success) throw new FormatException($"Invalid part format: {s}");

        return new()
        {
            X = int.Parse(match.Groups["x"].ValueSpan),
            M = int.Parse(match.Groups["m"].ValueSpan),
            A = int.Parse(match.Groups["a"].ValueSpan),
            S = int.Parse(match.Groups["s"].ValueSpan),
        };
    }
}

internal sealed class PuzzleInput
{
    public required IReadOnlyList<Workflow> Workflows { get; init; }
    public required IReadOnlyList<PartRatings> Parts { get; init; }
}

