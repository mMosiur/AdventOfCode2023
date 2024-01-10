namespace AdventOfCode.Year2023.Day05.ExtendedMath;

public static class RangeExtensions
{
	public static Range? Intersect(this Range range, Range other)
	{
		uint start = Math.Max(range.Start, other.Start);
		uint end = Math.Min(range.End, other.End);
		return start <= end ? new(start, end) : null;
	}

	public static (Range?, Range?) Remove(this Range range, Range other)
	{
		if (range.Start < other.Start)
		{
			if (range.End < other.Start)
			{
				return (range, null);
			}

			if (range.End <= other.End)
			{
				return (new(range.Start, other.Start - 1), null);
			}

			return (new(range.Start, other.Start - 1), new(other.End + 1, range.End));
		}

		if (range.Start > other.End)
		{
			return (range, null);
		}

		if (range.End <= other.End)
		{
			return (null, null);
		}

		return (null, new(other.End + 1, range.End));
	}

	public static Range MoveBy(this Range range, long offset)
	{
		uint newStart = (uint)(range.Start + offset);
		uint newEnd = (uint)(range.End + offset);
		return new(newStart, newEnd);
	}
}
