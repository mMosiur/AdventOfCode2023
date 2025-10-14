namespace AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

internal abstract class CommunicationModule
{
    public required string Name { get; init; }
    public required IReadOnlyList<string> Destinations { get; init; }
    public abstract CommunicationModuleType Type { get; }

    public abstract void Reset();
    public abstract void AddInput(string input);
    public abstract Pulse Process(string sourceName, Pulse input);
}
