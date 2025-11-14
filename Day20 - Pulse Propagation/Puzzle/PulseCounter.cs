using AdventOfCode.Year2023.Day20.Puzzle.Models;
using AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

namespace AdventOfCode.Year2023.Day20.Puzzle;

internal sealed class PulseCounter(MachineModules modules)
{
    private readonly MachineModules _modules = modules;

    public (int LowPulseCount, int HighPulseCount) CountPulses(int buttonPushes, Pulse buttonPulse)
    {
        _modules.Reset();

        int totalLowPulses = 0;
        int totalHighPulses = 0;

        var queue = new PulseQueue(_modules, buttonPulse);
        for (int i = 0; i < buttonPushes; i++)
        {
            int lowPulses = 0;
            int highPulses = 0;
            queue.PushButtonAndProcessPulses(pulsePath =>
            {
                if (pulsePath.Pulse is Pulse.Low) ++lowPulses;
                else if (pulsePath.Pulse is Pulse.High) ++highPulses;
            });
            totalLowPulses += lowPulses;
            totalHighPulses += highPulses;
        }

        return (totalLowPulses, totalHighPulses);
    }
}
