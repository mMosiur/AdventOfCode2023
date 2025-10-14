using System.Text.RegularExpressions;
using AdventOfCode.Common.StringExtensions;
using AdventOfCode.Year2023.Day20.Puzzle.Models;
using AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

namespace AdventOfCode.Year2023.Day20.Puzzle;

internal static partial class InputReader
{
    [GeneratedRegex(@"^(?'prefix'\%|\&|)(?'name'\w+) *- *> *(?'destinations'[\w, ]+)$")]
    private static partial Regex ModuleLineRegex();

    public static ModuleCollection Read(string input)
    {
        var modules = input.EnumerateLines()
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(ReadLine)
            .ToDictionary(m => m.Name);

        foreach (var module in modules.Values)
        {
            foreach (var destination in module.Destinations)
            {
                modules.GetValueOrDefault(destination)?.AddInput(module.Name);
            }
        }

        return new(modules);
    }

    private static CommunicationModule ReadLine(string line)
    {
        var match = ModuleLineRegex().Match(line);
        var name = match.Groups["name"].Value;
        var prefixSpan = match.Groups["prefix"].Value.AsSpan();
        var destinations = match.Groups["destinations"].Value.Split(",", StringSplitOptions.TrimEntries);
        var type = CommunicationModuleTypeHelper.FromPrefix(prefixSpan);

        return type switch
        {
            CommunicationModuleType.Broadcast => new BroadcasterModule
            {
                Name = name,
                Destinations = destinations,
            },
            CommunicationModuleType.FlipFlop => new FlipFlopModule
            {
                Name = name,
                Destinations = destinations,
            },
            CommunicationModuleType.Conjunction => new ConjunctionModule
            {
                Name = name,
                Destinations = destinations,
            },
            _ => throw new InvalidOperationException($"Unknown communication module type '{type}'"),
        };
    }
}
