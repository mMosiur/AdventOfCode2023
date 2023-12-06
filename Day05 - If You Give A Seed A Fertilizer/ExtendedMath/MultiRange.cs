using System.Collections;
using System.Diagnostics.Contracts;

namespace AdventOfCode.Year2023.Day05.ExtendedMath;

public sealed partial class MultiRange : IReadOnlyCollection<Range>, ICloneable
{
	private readonly List<Range> _ranges;

	public int Count => _ranges.Count;

	public MultiRange()
	{
		_ranges = new();
	}

	public MultiRange(int expectedCount)
	{
		_ranges = new(expectedCount);
	}

	public MultiRange(Range range)
		: this(1)
	{
		_ranges.Add(range);
	}

	private MultiRange(MultiRange other)
	{
		_ranges = new(other._ranges);
	}

	public MultiRange(IEnumerable<Range> ranges)
		: this(ranges.TryGetNonEnumeratedCount(out int count) ? count : 2)
	{
		foreach (var range in ranges)
		{
			Add(range);
		}
	}

	[Pure]
	public IEnumerator<Range> GetEnumerator() => _ranges.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


	public MultiRange Clone() => new(this);
	object ICloneable.Clone() => Clone();

	public override string ToString() => $"MultiRange ({string.Join(", ", _ranges)})";
}
