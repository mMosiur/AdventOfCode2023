using AdventOfCode.Year2023.Day20.Puzzle.Models;
using AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

namespace AdventOfCode.Year2023.Day20.Puzzle;

internal sealed class ModuleGroupAnalyzer(MachineModules modules)
{
    private readonly MachineModules _modules = modules;

    public Dictionary<ModuleGroupInfo, int> RetrieveGroupPeriods(ModuleGroupStructureInfo structureInfo, Pulse buttonPulse)
    {
        _modules.Reset();

        var queue = new PulseQueue(_modules, buttonPulse);
        int totalButtonPresses = 0;
        var groupPeriods = structureInfo.ModuleGroups.ToDictionary(mg => mg, mg => 0);
        while (groupPeriods.Values.Any(v => v == 0))
        {
            totalButtonPresses++;
            int currentButtonPress = totalButtonPresses;
            queue.PushButtonAndProcessPulses(pulsePath =>
            {
                if (pulsePath.Pulse is Pulse.Low || pulsePath.DestinationModule != structureInfo.GroupAggregatorModule)
                    return;
                var group = structureInfo.ModuleGroups
                                .FirstOrDefault(m => m.GroupOutputModule == pulsePath.SourceModule)
                         ?? throw new InvalidOperationException("Received pulse from unexpected module to group aggregator.");
                if (groupPeriods[group] == 0)
                {
                    groupPeriods[group] = currentButtonPress;
                }
                else if (groupPeriods[group] % currentButtonPress != 0)
                    throw new InvalidOperationException("Inconsistent group period detected.");
            });
        }

        return groupPeriods;
    }
}
