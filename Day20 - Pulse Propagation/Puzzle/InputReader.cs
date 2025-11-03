using System.Text.RegularExpressions;
using AdventOfCode.Common.StringExtensions;
using AdventOfCode.Year2023.Day20.Puzzle.Models;
using AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

namespace AdventOfCode.Year2023.Day20.Puzzle;

internal static partial class InputReader
{
    [GeneratedRegex(@"^(?'prefix'\%|\&|)(?'name'\w+) *- *> *(?'destinations'[\w, ]+)$")]
    private static partial Regex ModuleLineRegex();

    public static MachineModules Read(string input, string moduleConnectedToButtonName)
    {
        var modules = input.EnumerateLines()
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(ReadModuleInfo)
            .ToDictionary(m => m.Name);

        AddOutputOnlyModules(modules);

        foreach (var moduleInfo in modules.Values)
        {
            foreach (var destination in moduleInfo.Destinations)
            {
                if (!modules.TryGetValue(destination, out var destinationModuleInfo))
                {
                    throw new InvalidOperationException($"Module '{moduleInfo.Name}' has a destination to unknown module '{destination}'.");
                }

                destinationModuleInfo.Module.ConnectInput(moduleInfo.Module);
            }
        }

        return new(modules.Values.Select(m => m.Module), moduleConnectedToButtonName);
    }

    private static void AddOutputOnlyModules(Dictionary<string, ModuleInfo> modules)
    {
        var allDestinations = modules.Values
            .SelectMany(m => m.Destinations)
            .ToHashSet(StringComparer.Ordinal);

        foreach (var destination in allDestinations)
        {
            if (modules.ContainsKey(destination)) continue;
            var module = CreateModule(CommunicationModuleType.Broadcast, destination);
            modules.Add(destination, new(
                Name: destination,
                Module: module,
                Destinations: []
            ));
        }
    }

    private static ModuleInfo ReadModuleInfo(string line)
    {
        var match = ModuleLineRegex().Match(line);
        var name = match.Groups["name"].Value;
        var prefixSpan = match.Groups["prefix"].Value.AsSpan();
        var type = CommunicationModuleTypeHelper.FromPrefix(prefixSpan);
        var destinations = match.Groups["destinations"].Value.Split(",", StringSplitOptions.TrimEntries);
        return new(
            Name: name,
            Module: CreateModule(type, name),
            Destinations: destinations
        );
    }

    private static CommunicationModule CreateModule(CommunicationModuleType type, string name) =>
        type switch
        {
            CommunicationModuleType.Broadcast => new BroadcasterModule(name),
            CommunicationModuleType.Conjunction => new ConjunctionModule(name),
            CommunicationModuleType.FlipFlop => new FlipFlopModule(name),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Unsupported module type: {type}"),
        };

    private readonly record struct ModuleInfo(string Name, CommunicationModule Module, IEnumerable<string> Destinations);
}
