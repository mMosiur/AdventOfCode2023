namespace AdventOfCode.Year2023.Day12;

internal sealed class SpringArrangementMemo
{
	private int[,,] _memo = new int[0, 0, 0];

	public void ClearAndResizeFor(SpringRow row)
	{
		int nofSprings = row.ConditionRecords.Length;
		int nofGroups = row.DamagedGroupSizes.Length;
		int maxGroupSize = row.DamagedGroupSizes.Max();

		if (_memo.GetLength(0) < nofSprings || _memo.GetLength(1) < nofGroups || _memo.GetLength(2) <= maxGroupSize)
		{
			int newSpringsDimension = Math.Max(_memo.GetLength(0), nofSprings);
			int newGroupsDimension = Math.Max(_memo.GetLength(1), nofGroups);
			int newGroupSizeDimension = Math.Max(_memo.GetLength(2), maxGroupSize) + 1;
			_memo = new int[newSpringsDimension, newGroupsDimension, newGroupSizeDimension];
		}
		else
		{
			Array.Clear(_memo);
		}
	}

	public int this[int springIndex, int groupIndex, int groupSize]
	{
		get => _memo[springIndex, groupIndex, groupSize];
		set => _memo[springIndex, groupIndex, groupSize] = value;
	}
}
