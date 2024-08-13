using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2023.Day15;

public sealed class Day15Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 15;
	public override string Title => "Lens Library";

	private readonly IReadOnlyList<string> _initializationSequence;

	public Day15Solver(Day15SolverOptions options) : base(options)
	{
		_initializationSequence = InputReader.ReadInitializationSequence(Input);
	}

	public Day15Solver(Action<Day15SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day15Solver() : this(new Day15SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		int sumOfHashes = _initializationSequence.Sum(HolidayAsciiStringHelper.Hash);
		return sumOfHashes.ToString();
	}

	public override string SolvePart2()
	{
		var initializationSteps = _initializationSequence
			.Select(InitializationStep.Parse)
			.ToList();
		var boxes = new List<Lens>?[256];
		foreach (var step in initializationSteps)
		{
			int labelHash = HolidayAsciiStringHelper.Hash(step.Label);
			var box = boxes[labelHash];
			if (box is null)
			{
				box = new(1);
				boxes[labelHash] = box;
			}

			switch (step.Operation)
			{
				case InitializationOperationType.Remove:
					// find lens with the same label and remove it
					box.RemoveAll(lens => lens.Label == step.Label);
					break;
				case InitializationOperationType.Insert:
				{
					var lens = box.Find(l => l.Label == step.Label);
					if (lens is null)
					{
						lens = new(step.Label);
						box.Add(lens);
					}

					lens.FocalLength = step.Parameter;
					break;
				}
				default:
					throw new InvalidOperationException($"Invalid operation type '{step.Operation}'.");
			}
		}

		int focusingPowerSum = 0;
		for (int boxNumber = 0; boxNumber < boxes.Length; boxNumber++)
		{
			var box = boxes[boxNumber];
			if (box is null) continue;
			for (int slotNumber = 0; slotNumber < box.Count; slotNumber++)
			{
				var lens = box[slotNumber];
				focusingPowerSum += (boxNumber + 1) * (slotNumber + 1) * lens.FocalLength;
			}
		}

		return focusingPowerSum.ToString();
	}
}

internal class Lens(string label)
{
	public string Label { get; } = label;
	public int FocalLength { get; set; }
}
