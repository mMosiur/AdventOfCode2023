using AdventOfCode.Year2023.Day20.Puzzle.Models;
using AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

namespace AdventOfCode.Year2023.Day20;

internal sealed class PulseCounter(ModuleCollection modules)
{
    private const string ButtonModuleName = "BUTTON";

    private readonly ModuleCollection _modules = modules;

    public (int LowPulseCount, int HighPulseCount) CountPulses(string moduleConnectedToButton, int buttonPushes, Pulse buttonPulse)
    {
        if (!_modules.ContainsModule(moduleConnectedToButton))
        {
            throw new ArgumentException($"Start module '{moduleConnectedToButton}' not found in modules.", nameof(moduleConnectedToButton));
        }

        _modules.Reset();

        int lowPulseCount = 0;
        int highPulseCount = 0;
        Queue<PulsePath> queue = new();

        for (int i = 0; i < buttonPushes; i++)
        {
            queue.Enqueue(new(ButtonModuleName, moduleConnectedToButton, buttonPulse));

            while (queue.TryDequeue(out var pulePath))
            {
                if (pulePath.Pulse is Pulse.High) ++highPulseCount;
                if (pulePath.Pulse is Pulse.Low) ++lowPulseCount;

                if (!_modules.TryGetModule(pulePath.DestinationModuleName, out var destinationModule))
                {
                    continue; // Loose wiring
                }

                var outputPulse = destinationModule.Process(pulePath.SourceModuleName, pulePath.Pulse);
                if (outputPulse is Pulse.None) continue;

                foreach (var nextDestination in destinationModule.Destinations)
                {
                    queue.Enqueue(new(pulePath.DestinationModuleName, nextDestination, outputPulse));
                }
            }
        }

        return (lowPulseCount, highPulseCount);
    }

    private readonly record struct PulsePath(string SourceModuleName, string DestinationModuleName, Pulse Pulse);
}
