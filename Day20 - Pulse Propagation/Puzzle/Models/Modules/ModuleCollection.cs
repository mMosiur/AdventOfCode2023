using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

internal sealed class ModuleCollection(IReadOnlyDictionary<string, CommunicationModule> modules)
{
    private readonly IReadOnlyDictionary<string, CommunicationModule> _modules = modules;

    public bool ContainsModule(string moduleName)
    {
        return _modules.ContainsKey(moduleName);
    }

    public bool TryGetModule(string moduleName, [NotNullWhen(true)] out CommunicationModule? module)
    {
        return _modules.TryGetValue(moduleName, out module);
    }

    public void Reset()
    {
        foreach (var module in _modules.Values)
        {
            module.Reset();
        }
    }
}
