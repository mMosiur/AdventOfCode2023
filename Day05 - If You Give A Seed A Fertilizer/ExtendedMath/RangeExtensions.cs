namespace AdventOfCode.Year2023.Day05.ExtendedMath;

public static class RangeExtensions
{
	public static (Range? Left, Range? Middle, Range? Right) SplitUsing(this Range range, Range other)
	{
		uint start = Math.Max(range.Start, other.Start);
		uint end = Math.Min(range.End, other.End);
		Range? middle = start <= end ? new(start, end) : null;

		uint leftStart = range.Start;
		uint leftEnd = Math.Min(range.End, other.Start);
		Range? left = leftStart <= leftEnd ? new(leftStart, leftEnd) : null;

		uint rightStart = Math.Max(range.Start, other.End);
		uint rightEnd = range.End;
		Range? right = rightStart <= rightEnd ? new(rightStart, rightEnd) : null;

		return (left, middle, right);
	}

	public static Range MoveBy(this Range range, long offset)
	{
		uint newStart = (uint)(range.Start + offset);
		uint newEnd = (uint)(range.End + offset);
		return new(newStart, newEnd);
	}
}
