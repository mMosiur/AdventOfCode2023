using AdventOfCode.Year2023.Day20.Puzzle.Models;
using AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

namespace AdventOfCode.Year2023.Day20.Puzzle;

internal sealed class PulseCounter(MachineModules modules)
{
    private readonly MachineModules _modules = modules;

    public (int LowPulseCount, int HighPulseCount) CountPulses(int buttonPushes, Pulse buttonPulse)
    {
        _modules.Reset();

        int lowPulseCount = 0;
        int highPulseCount = 0;
        Queue<PulsePath> queue = new();

        for (int i = 0; i < buttonPushes; i++)
        {
            PropagatePulse(queue, _modules.ButtonModule, buttonPulse);

            while (queue.TryDequeue(out var pulsePath))
            {
                if (pulsePath.Pulse is Pulse.High) ++highPulseCount;
                else if (pulsePath.Pulse is Pulse.Low) ++lowPulseCount;

                var outputPulse = Process(pulsePath);
                PropagatePulse(queue, pulsePath.DestinationModule, outputPulse);
            }
        }

        return (lowPulseCount, highPulseCount);
    }

    private static int PropagatePulse(Queue<PulsePath> queue, CommunicationModule sourceModule, Pulse pulse)
    {
        if (pulse is Pulse.None) return 0;

        foreach (var destination in sourceModule.DestinationsModules)
        {
            queue.Enqueue(new(sourceModule, destination.Value, pulse));
        }

        return sourceModule.DestinationsModules.Count;
    }

    private static Pulse Process(PulsePath pulsePath)
    {
        return pulsePath.DestinationModule.Process(pulsePath.SourceModule.Name, pulsePath.Pulse);
    }

    private readonly record struct PulsePath(CommunicationModule SourceModule, CommunicationModule DestinationModule, Pulse Pulse);
}
