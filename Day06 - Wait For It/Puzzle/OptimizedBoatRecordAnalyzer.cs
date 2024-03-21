namespace AdventOfCode.Year2023.Day06.Puzzle;

internal sealed class OptimizedBoatRecordAnalyzer
{
	private readonly BoatRecord _record;

	public OptimizedBoatRecordAnalyzer(BoatRecord record)
	{
		_record = record;
	}

	public int CountWaysToBeatRecord()
	{
		// Optimize using binary search - since all ways to beat record will be from a continuous range of time holding button,
		// we can find the first time to beat the record using binary search and then find the last time using binary search
		// and simply subtract the two indices to get the count of ways to beat the record.
		int firstTimeToBeatRecord = FindFirstTimeToBeatRecord();
		if (firstTimeToBeatRecord == -1) return 0; // Can't beat the record
		int lastTimeToBeatRecord = FindLastTimeToBeatRecord();
		if (lastTimeToBeatRecord == -1) return 0; // Can't beat the record
		return lastTimeToBeatRecord - firstTimeToBeatRecord + 1;
	}

	private long GetDistanceFromTimeButtonHeld(long timeHoldingButton)
	{
		long timeLeft = _record.Time - timeHoldingButton;
		long speed = timeHoldingButton;
		return speed * timeLeft;
	}

	private (bool HoldingButtonBeatsRecord, bool HoldingButtonLongerBeatsRecord) CheckIfHoldingButtonBreaksRecord(long timeHoldingButton)
	{
		long distance1 = GetDistanceFromTimeButtonHeld(timeHoldingButton);
		bool recordBroken1 = distance1 > _record.Distance;
		long distance2 = GetDistanceFromTimeButtonHeld(timeHoldingButton + 1);
		bool recordBroken2 = distance2 > _record.Distance;
		return (recordBroken1, recordBroken2);
	}

	private int FindFirstTimeToBeatRecord()
	{
		int lowerIndex = 1;
		int upperIndex = (int)_record.Time;
		while (lowerIndex < upperIndex)
		{
			int mid = lowerIndex + (upperIndex - lowerIndex) / 2;

			(bool holdingButtonBeatsRecord, bool holdingButtonLongerBeatsRecord) = CheckIfHoldingButtonBreaksRecord(mid);

			if (holdingButtonBeatsRecord)
			{
				// Either both mid and mid + 1 beat record or mid is the last time to beat the record, either way since we look for the first time we need to go lower half
				upperIndex = mid;
			}
			else if (holdingButtonLongerBeatsRecord)
			{
				// Mid does not beat record (previous if) but mid + 1 does, so mid + 1 is the first time to beat the record
				return mid + 1;
			}
			else
			{
				// Both mid and mid + 1 do not beat record, so we need to go to upper half
				lowerIndex = mid + 1;
			}
		}

		return -1;
	}

	private int FindLastTimeToBeatRecord()
	{
		int lowerIndex = 1;
		int upperIndex = (int)_record.Time;
		while (lowerIndex < upperIndex)
		{
			int mid = lowerIndex + (upperIndex - lowerIndex) / 2;

			(bool holdingButtonBeatsRecord, bool holdingButtonLongerBeatsRecord) = CheckIfHoldingButtonBreaksRecord(mid);

			if (holdingButtonLongerBeatsRecord)
			{
				// Either both mid and mid + 1 beat record or mid + 1 is the first time to beat the record, either way since we look for the last time we need to go upper half
				lowerIndex = mid + 1;
			}
			else if (holdingButtonBeatsRecord)
			{
				// Mid beats record (previous if) but mid + 1 does not, so mid is the last time to beat the record
				return mid;
			}
			else
			{
				// Both mid and mid + 1 do not beat record, so we need to go to lower half
				upperIndex = mid;
			}
		}

		return -1;
	}
}
