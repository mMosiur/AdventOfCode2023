using AdventOfCode.Year2023.Day20.Puzzle.Models;
using AdventOfCode.Year2023.Day20.Puzzle.Models.Modules;

namespace AdventOfCode.Year2023.Day20.Puzzle;

internal sealed class PulseQueue(MachineModules modules, Pulse buttonPulse)
{
    private readonly MachineModules _modules = modules;
    private readonly Pulse _buttonPulse = buttonPulse;
    private readonly Queue<PulsePath> _queue = new();

    public void PushButtonAndProcessPulses(Action<PulsePath>? handlePulsePath = null)
    {
        _queue.Enqueue(new(null!, _modules.EntryModule, _buttonPulse));

        while (_queue.TryDequeue(out var pulsePath))
        {
            handlePulsePath?.Invoke(pulsePath);
            ProcessPulsePath(pulsePath);
        }
    }

    private void ProcessPulsePath(PulsePath pulsePath)
    {
        var outputPulse = pulsePath.DestinationModule.Process(pulsePath.SourceModule, pulsePath.Pulse);
        if (outputPulse is Pulse.None) return;
        foreach (var destination in pulsePath.DestinationModule.Destinations)
        {
            _queue.Enqueue(new(pulsePath.DestinationModule, destination.Value, outputPulse));
        }
    }

    public readonly record struct PulsePath(
        CommunicationModule SourceModule,
        CommunicationModule DestinationModule,
        Pulse Pulse);
}
