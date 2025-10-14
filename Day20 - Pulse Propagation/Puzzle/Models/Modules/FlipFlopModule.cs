namespace AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

internal sealed class FlipFlopModule : CommunicationModule
{
    private const bool InitialState = false;

    private bool _state = InitialState;

    public override CommunicationModuleType Type => CommunicationModuleType.FlipFlop;

    public override void Reset()
    {
        _state = InitialState;
    }

    public override void AddInput(string input)
    {
        // Info about input is not needed for flip-flops
    }

    public override Pulse Process(string sourceName, Pulse input)
    {
        if (input is not Pulse.Low) return Pulse.None;
        _state = !_state;
        return _state ? Pulse.High : Pulse.Low;
    }
}
