namespace AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

internal sealed class BroadcasterModule(string name)
    : CommunicationModule(name)
{
    public override CommunicationModuleType Type => CommunicationModuleType.Broadcast;

    public override void Reset()
    {
        // No state to reset for broadcasters
    }

    public override Pulse Process(CommunicationModule source, Pulse input)
    {
        return input;
    }
}
