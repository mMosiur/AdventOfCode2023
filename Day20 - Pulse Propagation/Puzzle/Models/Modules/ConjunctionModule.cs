namespace AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

internal sealed class ConjunctionModule(string name)
    : CommunicationModule(name)
{
    private const Pulse InitialState = Pulse.Low;

    private readonly Dictionary<CommunicationModule, Pulse> _mostRecentInputs = new();

    public override CommunicationModuleType Type => CommunicationModuleType.Conjunction;

    public override void Reset()
    {
        foreach (CommunicationModule key in _mostRecentInputs.Keys)
        {
            _mostRecentInputs[key] = InitialState;
        }
    }

    public override void ConnectInput(CommunicationModule inputModule)
    {
        base.ConnectInput(inputModule);
        _mostRecentInputs.Add(inputModule, InitialState);
    }

    public override Pulse Process(CommunicationModule source, Pulse input)
    {
        _mostRecentInputs[source] = input;
        return _mostRecentInputs.Values.All(v => v is Pulse.High)
            ? Pulse.Low
            : Pulse.High;
    }
}
