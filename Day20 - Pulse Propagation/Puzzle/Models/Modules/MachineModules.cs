namespace AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

internal sealed class MachineModules
{
    private const string ButtonModuleName = "BUTTON";

    private readonly Dictionary<string, CommunicationModule> _modules;

    public CommunicationModule ButtonModule { get; }

    public MachineModules(IEnumerable<CommunicationModule> modules, string moduleConnectedToButtonName)
    {
        _modules = modules.ToDictionary(m => m.Name);
        if (!_modules.TryGetValue(moduleConnectedToButtonName, out var moduleConnectedToButton))
        {
            throw new ArgumentException($"Module collection does not contain a module named '{moduleConnectedToButtonName}'.", nameof(moduleConnectedToButtonName));
        }

        ButtonModule = new BroadcasterModule(ButtonModuleName);
        moduleConnectedToButton.ConnectInput(ButtonModule);
        _modules.Add(ButtonModuleName, ButtonModule);
    }

    public void Reset()
    {
        foreach (var module in _modules.Values)
        {
            module.Reset();
        }
    }
}
