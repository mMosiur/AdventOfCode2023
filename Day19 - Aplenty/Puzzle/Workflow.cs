namespace AdventOfCode.Year2023.Day19.Puzzle;

internal sealed class Workflow
{
    public required string Name { get; init; }
    public required IReadOnlyList<Rule> Rules { get; init; }

    public static Workflow FinalWorkflow(string name) => new()
    {
        Name = name,
        Rules = [],
    };
}

internal abstract class Rule
{
    public required string Destination { get; init; }
}

internal sealed class ConditionalRule : Rule
{
    public required RatingCategory Category { get; init; }
    public required Operator Comparison { get; init; }
    public required int Value { get; init; }
}

internal sealed class TerminatingRule : Rule;

internal enum Operator
{
    LessThan = 1,
    GreateThan = 2,
}

internal enum RatingCategory
{
    X = 1, // eXtremely cool looking
    M = 2, // Musical (it makes a noise when you hit it)
    A = 3, // Aerodynamic
    S = 4, // Shiny
}
