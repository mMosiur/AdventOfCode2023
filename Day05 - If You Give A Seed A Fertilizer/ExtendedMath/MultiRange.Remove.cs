namespace AdventOfCode.Year2023.Day05.ExtendedMath;

public sealed partial class MultiRange
{
	public bool Remove(Range rangeToRemove)
	{
		bool removed = false;
		for (int i = 0; i < _ranges.Count; i++)
		{
			var currentRange = _ranges[i];
			if (currentRange.End < rangeToRemove.Start)
			{
				continue;
			}
			if (currentRange.Start > rangeToRemove.End)
			{
				break;
			}
			removed = true;

			var (left1, left2) = currentRange.Remove(rangeToRemove);

			if (left1 is null && left2 is null)
			{
				_ranges.RemoveAt(i);
				i--;
			}
			else if (left1 is not null && left2 is null)
			{
				_ranges[i] = left1.Value;
			}
			else if (left1 is null && left2 is not null)
			{
				_ranges[i] = left2.Value;
			}
			else
			{
				_ranges[i] = left1!.Value;
				i++;
				_ranges.Insert(i, left2!.Value);
			}
		}

		return removed;
	}
}
