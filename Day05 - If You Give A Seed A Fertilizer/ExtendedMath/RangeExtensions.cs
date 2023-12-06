namespace AdventOfCode.Year2023.Day05.ExtendedMath;

public static class RangeExtensions
{
	public static Range? Intersect(this Range range, Range other)
	{
		uint start = Math.Max(range.Start, other.Start);
		uint end = Math.Min(range.End, other.End);
		return start <= end ? new(start, end) : null;
	}

	public static Range MoveBy(this Range range, long offset)
	{
		uint newStart = (uint)(range.Start + offset);
		uint newEnd = (uint)(range.End + offset);
		return new(newStart, newEnd);
	}
}
