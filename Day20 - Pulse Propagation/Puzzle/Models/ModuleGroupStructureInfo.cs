using AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

namespace AdventOfCode.Year2023.Day20.Puzzle.Models;

internal sealed class ModuleGroupStructureInfo
{
    public required List<ModuleGroupInfo> ModuleGroups { get; init; }
    public required CommunicationModule GroupAggregatorModule { get; init; }
}
