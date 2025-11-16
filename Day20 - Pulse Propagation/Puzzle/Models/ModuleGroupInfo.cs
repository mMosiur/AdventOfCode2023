using AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

namespace AdventOfCode.Year2023.Day20.Puzzle.Models;

internal sealed class ModuleGroupInfo
{
    public required CommunicationModule GroupStartModule { get; init; }
    public required IReadOnlySet<CommunicationModule> GroupModules { get; init; }
    public required CommunicationModule GroupOutputModule { get; init; }
}
