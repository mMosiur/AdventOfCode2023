namespace AdventOfCode.Year2023.Day06.Puzzle;

internal sealed class BoatRecordAnalyzer
{
	public int CountWaysToBeatRecord(BoatRecord record)
	{
		int count = 0;
		for (int timeHoldingButton = 1; timeHoldingButton < record.Time; timeHoldingButton++)
		{
			long timeLeft = record.Time - timeHoldingButton;
			long speed = timeHoldingButton;
			long distance = speed * timeLeft;
			if (distance > record.Distance)
			{
				count++;
			}
		}
		return count;
	}
}
