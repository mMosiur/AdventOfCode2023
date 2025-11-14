using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

internal sealed class MachineModules
{
    private readonly Dictionary<string, CommunicationModule> _modules;

    public CommunicationModule EntryModule { get; }

    public MachineModules(IEnumerable<CommunicationModule> modules, string entryModuleName)
    {
        _modules = modules.ToDictionary(m => m.Name);
        if (!_modules.TryGetValue(entryModuleName, out var entryModule))
        {
            throw new ArgumentException($"Module collection does not contain a module named '{entryModuleName}'.", nameof(entryModuleName));
        }

        EntryModule = entryModule;
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
