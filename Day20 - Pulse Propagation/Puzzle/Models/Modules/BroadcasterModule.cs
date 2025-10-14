namespace AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

internal sealed class BroadcasterModule : CommunicationModule
{
    public override CommunicationModuleType Type => CommunicationModuleType.Broadcast;

    public override void Reset()
    {
        // No state to reset for broadcasters
    }

    public override void AddInput(string input)
    {
        // Info about input is not needed for broadcasters
    }

    public override Pulse Process(string sourceName, Pulse input)
    {
        return input;
    }
}
