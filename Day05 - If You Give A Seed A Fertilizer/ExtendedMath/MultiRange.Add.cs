namespace AdventOfCode.Year2023.Day05.ExtendedMath;

public sealed partial class MultiRange
{
	public void Add(Range range)
	{
		uint newRangeStart = range.Start;
		uint newRangeEnd = range.End;

		int index = 0;
		// Find the position to insert and handle merging as needed
		while (index < _ranges.Count && _ranges[index].End + 1 < range.Start)
		{
			index++;
		}

		int rangesToRemove = 0;
		int firstIndexToRemove = -1;
		// Check for overlap and merge if needed
		while (index < _ranges.Count && _ranges[index].Start <= range.End)
		{
			newRangeStart = Math.Min(newRangeStart, _ranges[index].Start);
			newRangeEnd = Math.Max(newRangeEnd, _ranges[index].End);

			if (firstIndexToRemove == -1) firstIndexToRemove = index;
			rangesToRemove++;

			index++;
		}

		// Remove all overlapping ranges at once
		if (rangesToRemove > 0) _ranges.RemoveRange(firstIndexToRemove, rangesToRemove);

		// Insert the merged or original range at the appropriate position
		_ranges.Insert(index - rangesToRemove, new(newRangeStart, newRangeEnd));
	}

	public void Add(IEnumerable<Range> ranges)
	{
		foreach (var range in ranges)
		{
			Add(range);
		}
	}
}
