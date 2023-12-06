namespace AdventOfCode.Year2023.Day05.ExtendedMath;

public sealed partial class MultiRange
{
	public bool Remove(Range rangeToRemove, out string s)
	{
		bool removed = false;
		int index = 0;

		while (index < _ranges.Count && _ranges[index].End < rangeToRemove.Start)
		{
			index++;
		}

		while (index < _ranges.Count && _ranges[index].Start <= rangeToRemove.End)
		{
			if (_ranges[index].Start >= rangeToRemove.Start && _ranges[index].End <= rangeToRemove.End)
			{
				// This happens when the range to remove is larger than the current range and the current range is completely removed.
				_ranges.RemoveAt(index);
				removed = true;
			}
			else if (_ranges[index].Start < rangeToRemove.Start && _ranges[index].End > rangeToRemove.End)
			{
				// This happens when the range to remove is smaller than the current range and the current range is split into two.
				_ranges.Insert(index + 1, new(rangeToRemove.End + 1, _ranges[index].End));
				_ranges[index] = new(_ranges[index].Start, rangeToRemove.Start - 1);
				removed = true;
			}
			else if (_ranges[index].Start < rangeToRemove.Start && _ranges[index].End >= rangeToRemove.Start)
			{
				_ranges[index] = new(_ranges[index].Start, rangeToRemove.Start - 1);
				removed = true;
			}
			else if (_ranges[index].Start <= rangeToRemove.End && _ranges[index].End > rangeToRemove.End)
			{
				_ranges[index] = new(rangeToRemove.End + 1, _ranges[index].End);
				removed = true;
			}

			index++;
		}

		s += "6";

		return removed;
	}
}
