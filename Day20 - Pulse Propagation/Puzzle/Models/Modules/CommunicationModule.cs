using System.Diagnostics;

namespace AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
internal abstract class CommunicationModule(string name)
{
    private readonly Dictionary<string, CommunicationModule> _inputs = new();
    private readonly Dictionary<string, CommunicationModule> _destinations = new();

    public string Name { get; } = name;
    public abstract CommunicationModuleType Type { get; }
    public IReadOnlyDictionary<string, CommunicationModule> Inputs => _inputs;
    public IReadOnlyDictionary<string, CommunicationModule> Destinations => _destinations;

    public virtual void ConnectInput(CommunicationModule inputModule)
    {
        _inputs.Add(inputModule.Name, inputModule);
        inputModule._destinations.Add(Name, this);
    }

    public abstract void Reset();

    public abstract Pulse Process(CommunicationModule source, Pulse input);

    private string GetDebuggerDisplay() => $"{Name} (in {_inputs.Count}, out {_destinations.Count})";
}
