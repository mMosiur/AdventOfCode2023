namespace AdventOfCode.Year2023.Day15;

internal sealed class LensConfigurator
{
	private readonly List<Lens>?[] _boxes = new List<Lens>?[256];

	private List<Lens> GetBox(int boxNumber)
	{
		var box = _boxes[boxNumber];
		if (box is null)
		{
			box = new(1);
			_boxes[boxNumber] = box;
		}

		return box;
	}

	public void PerformStep(InitializationStep step)
	{
		int labelHash = HolidayAsciiStringHelper.Hash(step.Label);
		var box = GetBox(labelHash);

		switch (step.Operation)
		{
			case InitializationOperationType.Remove:
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
				throw new InvalidOperationException($"Invalid operation to perform: {step.Operation}.");
		}
	}

	public int CalculateFocusingPower()
	{
		int totalFocusingPower = 0;
		for (int boxNumber = 0; boxNumber < _boxes.Length; boxNumber++)
		{
			var box = _boxes[boxNumber];
			if (box is null) continue;
			for (int slotNumber = 0; slotNumber < box.Count; slotNumber++)
			{
				var lens = box[slotNumber];
				totalFocusingPower += CalculateSingleLensFocusingPower(boxNumber, slotNumber, lens);
			}
		}

		return totalFocusingPower;
	}

	private static int CalculateSingleLensFocusingPower(int boxNumber, int slotNumber, Lens lens)
	{
		return (boxNumber + 1) * (slotNumber + 1) * lens.FocalLength;
	}

	private sealed class Lens(string label)
	{
		public string Label { get; } = label;
		public int FocalLength { get; set; }
	}
}
