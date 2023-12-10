using System.Collections;

namespace AdventOfCode.Year2023.Day09.Puzzle.Oasis;

internal sealed class ValueHistory(IEnumerable<int> values)
	: IReadOnlyList<int>
{
	private readonly int[] _valueHistory = values.ToArray();
	private int? _nextValue;

	public int Count => _valueHistory.Length;

	public int this[int index] => _valueHistory[index];

	public IEnumerator<int> GetEnumerator() => _valueHistory.AsEnumerable().GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public int PredictNextValue()
	{
		_nextValue ??= InternalPredictNextValue();
		return _nextValue.Value;
	}

	private int InternalPredictNextValue()
	{
		Span<int> span = stackalloc int[Count];
		_valueHistory.CopyTo(span);

		int end = Count;

		while (true)
		{
			end--;
			int anyValueNonZero = 0;
			for (int i = 0; i < end; i++)
			{
				int diff = span[i + 1] - span[i];
				anyValueNonZero |= diff;
				span[i] = diff;
			}

			if (anyValueNonZero == 0) break;
		}

		while (end < Count)
		{
			span[end] = span[end - 1] + span[end];
			end++;
		}

		return span[^1];
	}
}
