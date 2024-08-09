namespace AdventOfCode.Year2023.Day14;

internal sealed class RockFormationTilter
{
	public RockFormation RockFormation { get; }
	public long NumberOfTilts { get; private set; }

	public RockFormationTilter(RockFormation rockFormation)
	{
		RockFormation = rockFormation;
		NumberOfTilts = 0;
	}

	public void TiltTo(Direction direction)
	{
		switch (direction)
		{
			case Direction.North:
				RockFormation.TiltToSlideNorth();
				break;
			case Direction.West:
				RockFormation.TiltToSlideWest();
				break;
			case Direction.South:
				RockFormation.TiltToSlideSouth();
				break;
			case Direction.East:
				RockFormation.TiltToSlideEast();
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(direction), direction, "Invalid direction");
		}

		NumberOfTilts++;
	}

	public void TiltInCycle()
	{
		RockFormation.TiltToSlideNorth();
		RockFormation.TiltToSlideWest();
		RockFormation.TiltToSlideSouth();
		RockFormation.TiltToSlideEast();
		NumberOfTilts += 4;
	}

	public void TiltInCycle(int numberOfCycles)
	{
		Dictionary<int, int> hashes = new();
		for (int i = 0; i < numberOfCycles; i++)
		{
			TiltInCycle();
			int hash = RockFormation.GetPlaneHash();
			if (hashes.TryAdd(hash, i))
			{
				continue;
			}
			int cycleLength = i - hashes[hash];
			int remainingCycles = numberOfCycles - i - 1;
			int cycleOffset = remainingCycles % cycleLength;
			for (int j = 0; j < cycleOffset; j++)
			{
				TiltInCycle();
			}
			remainingCycles -= cycleOffset;
			NumberOfTilts += remainingCycles;
			break;
		}
	}
}
