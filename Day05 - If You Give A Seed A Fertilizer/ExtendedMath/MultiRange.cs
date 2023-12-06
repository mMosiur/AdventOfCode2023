using System.Collections;

namespace AdventOfCode.Year2023.Day05.ExtendedMath;

public sealed class MultiRange : IEnumerable<Range>
{
	private readonly List<Range> _ranges;

	public MultiRange()
	{
		_ranges = new();
	}

	public MultiRange(Range range)
	{
		_ranges = new(1) { range };
	}

	public MultiRange(IEnumerable<Range> ranges)
	{
		_ranges = ranges.TryGetNonEnumeratedCount(out int count) ? new(count) : new(2);
		foreach (var range in ranges)
		{
			Add(range);
		}
	}


	public void Add(Range range)
	{
		uint newRangeStart = range.Start;
		uint newRangeEnd = range.End;

		int index = 0;
		// Find the position to insert and handle merging as needed
		while (index < _ranges.Count && _ranges[index].End < range.Start)
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

	public bool Remove(Range rangeToRemove)
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
				_ranges.RemoveAt(index);
				removed = true;
			}
			else if (_ranges[index].Start < rangeToRemove.Start && _ranges[index].End > rangeToRemove.End)
			{
				_ranges.Insert(index + 1, new Range(rangeToRemove.End + 1, _ranges[index].End));
				_ranges[index] = new Range(_ranges[index].Start, rangeToRemove.Start - 1);
				removed = true;
			}
			else if (_ranges[index].Start < rangeToRemove.Start && _ranges[index].End >= rangeToRemove.Start)
			{
				_ranges[index] = new Range(_ranges[index].Start, rangeToRemove.Start - 1);
				removed = true;
			}
			else if (_ranges[index].Start <= rangeToRemove.End && _ranges[index].End > rangeToRemove.End)
			{
				_ranges[index] = new Range(rangeToRemove.End + 1, _ranges[index].End);
				removed = true;
			}

			index++;
		}

		return removed;
	}


	public IEnumerator<Range> GetEnumerator() => _ranges.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
