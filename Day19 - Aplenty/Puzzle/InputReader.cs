using System.Text.RegularExpressions;
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
        using var it = input.EnumerateLines().GetEnumerator();
        it.EnsureMoveToNextNonEmptyLine("Expected non-empty line for number of workflows.");

        var workflows = new List<Workflow>();
        while (!string.IsNullOrWhiteSpace(it.Current))
        {
            workflows.Add(ParseWorkflow(it.Current));
            it.EnsureMoveNext();
        }

        it.EnsureMoveToNextNonEmptyLine();

        var parts = new List<Part>();
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
                Category = match.Groups["category"].Captures[i].ValueSpan switch
                {
                    "x" => Category.X,
                    "m" => Category.M,
                    "a" => Category.A,
                    "s" => Category.S,
                    _ => throw new FormatException("Invalid category.")
                },
                Comparison = match.Groups["operator"].Captures[i].ValueSpan switch
                {
                    "<" => Operator.LessThan,
                    ">" => Operator.GreateThan,
                    _ => throw new FormatException("Invalid operator.")
                },
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

    private static Part ParsePart(string s)
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

public sealed class PuzzleInput
{
    public required IReadOnlyList<Workflow> Workflows { get; init; }
    public required IReadOnlyList<Part> Parts { get; init; }
}

public sealed class Workflow
{
    public required string Name { get; init; }
    public required IReadOnlyList<Rule> Rules { get; init; }

    public static Workflow FinalWorkflow(string name) => new()
    {
        Name = name,
        Rules = [],
    };

    public override string ToString()
    {
        return $"{Name}{{{string.Join(", ", Rules)}}}";
    }
}

public abstract class Rule
{
    public required string Destination { get; init; }
}

public sealed class ConditionalRule : Rule
{
    public required Category Category { get; init; }
    public required Operator Comparison { get; init; }
    public required int Value { get; init; }

    public override string ToString()
    {
        var comparisonSymbol = Comparison switch
        {
            Operator.LessThan => "<",
            Operator.GreateThan => ">",
            _ => throw new InvalidOperationException()
        };
        return $"{Category} {comparisonSymbol} {Value} = {Destination}";
    }
}

public sealed class TerminatingRule : Rule
{
    public override string ToString()
    {
        return $"{Destination}";
    }
}

public sealed class Part
{
    public required int X { get; init; }
    public required int M { get; init; }
    public required int A { get; init; }
    public required int S { get; init; }

    public override string ToString()
    {
        return $"{{x={X},m={M},a={A},s={S}}}";
    }
}

public enum Operator
{
    LessThan = 1,
    GreateThan = 2,
}

public enum Category
{
    X = 1,
    M = 2,
    A = 3,
    S = 4,
}
