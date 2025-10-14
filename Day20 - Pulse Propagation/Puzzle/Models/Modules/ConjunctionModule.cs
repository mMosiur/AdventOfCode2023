namespace AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

internal sealed class ConjunctionModule : CommunicationModule
{
    private const Pulse InitialState = Pulse.Low;

    private readonly Dictionary<string, Pulse> _mostRecentInputs = new();

    public override CommunicationModuleType Type => CommunicationModuleType.Conjunction;

    public override void Reset()
    {
        foreach (string key in _mostRecentInputs.Keys)
        {
            _mostRecentInputs[key] = InitialState;
        }
    }

    public override void AddInput(string input)
    {
        _mostRecentInputs.Add(input, InitialState);
    }

    public override Pulse Process(string sourceName, Pulse input)
    {
        _mostRecentInputs[sourceName] = input;
        return _mostRecentInputs.Values.All(v => v is Pulse.High)
            ? Pulse.Low
            : Pulse.High;
    }
}
