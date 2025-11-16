using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Common;
using AdventOfCode.Year2023.Day20.Puzzle.Models;
using AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

namespace AdventOfCode.Year2023.Day20.Puzzle;

internal sealed class ModulesGrouper(MachineModules modules)
{
    private readonly MachineModules _modules = modules;

    public bool TryGenerateModuleGroups(string finalModuleName, [NotNullWhen(true)] out ModuleGroupStructureInfo? groupStructureInfo)
    {
        try
        {
            var finalModule = GetFinalModule(finalModuleName);
            var groupAggregatorModule = GetGroupAggregatorModule(finalModule);
            var groupStartModules = GetGroupStartModules(groupAggregatorModule);
            var groups = BuildModuleGroups(groupStartModules, groupAggregatorModule);
            AssertGroupsDontOverlap(groups);

            groupStructureInfo = new()
            {
                ModuleGroups = groups,
                GroupAggregatorModule = groupAggregatorModule,
            };
            return true;
        }
        catch (DaySolverException e)
        {
            groupStructureInfo = null;
            return false;
        }
    }

    private CommunicationModule GetFinalModule(string finalModuleName)
    {
        if (!_modules.TryGetModule(finalModuleName, out var finalModule))
            throw new DaySolverException($"Module collection does not contain a module named '{finalModuleName}'.");

        if (finalModule.Destinations.Count != 0)
            throw new DaySolverException("Final module is supposed to have no destinations.");

        return finalModule;
    }

    private static ConjunctionModule GetGroupAggregatorModule(CommunicationModule finalModule)
    {
        try
        {
            return finalModule.Inputs.Values.Single() as ConjunctionModule
                ?? throw new DaySolverException("Group aggregator module is not a conjunction module.");
        }
        catch (InvalidOperationException e)
        {
            throw new DaySolverException("Final module is supposed to have exactly one input.", e);
        }
    }

    private List<CommunicationModule> GetGroupStartModules(ConjunctionModule groupAggregatorModule)
    {
        var groupStartModules = _modules.EntryModule.Destinations.Values.ToList();
        var groupFinalModules = groupAggregatorModule.Inputs.Values.ToList();
        if (groupStartModules.Count != groupFinalModules.Count)
            throw new DaySolverException("Number of start modules does not match number of final modules.");

        return groupStartModules;
    }

    private static List<ModuleGroupInfo> BuildModuleGroups(List<CommunicationModule> groupStartModules, ConjunctionModule groupAggregatorModule)
    {
        return groupStartModules
            .Select(gsm => GenerateModuleGroup(gsm, groupAggregatorModule))
            .ToList();
    }

    private static void AssertGroupsDontOverlap(List<ModuleGroupInfo> groups)
    {
        for (int i = 0; i < groups.Count; i++)
        {
            var group1Modules = groups[i].GroupModules;
            for (int j = i + 1; j < groups.Count; j++)
            {
                var group2Modules = groups[j].GroupModules;
                if (group1Modules.Overlaps(group2Modules))
                    throw new DaySolverException("Groups overlap.");
            }
        }
    }

    private static ModuleGroupInfo GenerateModuleGroup(CommunicationModule groupStartModule, ConjunctionModule groupAggregatorModule)
    {
        var groupModules = new HashSet<CommunicationModule>();

        var moduleQueue = new Queue<CommunicationModule>();
        CommunicationModule? groupOutputModule = null;
        moduleQueue.Enqueue(groupStartModule);
        while (moduleQueue.TryDequeue(out var module))
        {
            if (!groupModules.Add(module)) continue; // already processed
            foreach (var destination in module.Destinations.Values)
            {
                if (destination == groupAggregatorModule)
                {
                    groupOutputModule = module;
                    continue;
                }

                moduleQueue.Enqueue(destination);
            }
        }

        if (groupOutputModule == null)
            throw new DaySolverException("Group output module not found.");

        return new()
        {
            GroupStartModule = groupStartModule,
            GroupModules = groupModules,
            GroupOutputModule = groupOutputModule,
        };
    }
}
