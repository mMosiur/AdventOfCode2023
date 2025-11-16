using AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

namespace AdventOfCode.Year2023.Day20.Puzzle.Models;

internal sealed class ModuleGroupStructureInfo
{
    public required List<ModuleGroupInfo> ModuleGroups { get; init; }

    /// <summary>
    /// Conjunction module that aggregates the final modules.
    /// Outputs <see cref="Pulse.Low"/> when all groups output <see cref="Pulse.High"/>.
    /// </summary>
    public required ConjunctionModule GroupAggregatorModule { get; init; }
}
