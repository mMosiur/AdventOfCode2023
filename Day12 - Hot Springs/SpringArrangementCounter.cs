using AdventOfCode.Common;

namespace AdventOfCode.Year2023.Day12;

internal sealed class SpringArrangementCounter
{
	private readonly SpringArrangementMemo _memo = new();

	public long CountPossibleArrangements(SpringRow row)
	{
		// Extend row with one operational spring at the end, with that making sure that the last spring cannot
		// be part of a damaged group, and so we can add a group size of size 0 at the end also
		row = new(
			[.. row.ConditionRecords, SpringCondition.Operational],
			[.. row.DamagedGroupSizes, 0]
		);

		_memo.ClearAndResizeFor(row);

		for (int springIndex = 0; springIndex < row.ConditionRecords.Length; springIndex++)
		{
			// Spring index represents the index in condition records of the spring currently being considered
			for (int groupIndex = 0; groupIndex < row.DamagedGroupSizes.Length; groupIndex++)
			{
				// Group index represents the index of damage group currently being considered
				for (int groupSize = 0; groupSize <= row.DamagedGroupSizes[groupIndex]; groupSize++)
				{
					// Group size represents the current size of the damage group currently being considered (it may have
					// just started with current group size of 0 all the way to just ended with the full group size)
					_memo[springIndex, groupIndex, groupSize] = CalculatePossibleArrangementsAt(springIndex, groupIndex, groupSize, row);
					// We can safely use CalculatePossibleArrangementsAt as we are at either springIndex = 0,
					// or all values for previous spring springIndex - 1 are already calculated and stored in memo
					// since we use that as the outer loop variable.
				}
			}
		}

		// Final answer is stored for the last spring (index = row.ConditionRecords.Length - 1),
		// last group (index = row.DamagedGroupSizes.Length - 1)
		// and with the last group size of 0 - we started the function with adding that 0 size group at the end.
		return _memo[row.ConditionRecords.Length - 1, row.DamagedGroupSizes.Length - 1, 0];
	}

	/// <summary>
	/// Calculated number of possible arrangements at the given spring index, group index and group size provided that
	/// all possible arrangements for previous spring (at currentSpringIndex - 1) are already stored in the memo (base
	/// cases for currentSpringIndex = 0 are handled correctly as well).
	/// </summary>
	private long CalculatePossibleArrangementsAt(int currentSpringIndex, int currentGroupIndex, int currentGroupSize, SpringRow row)
	{
		var springCondition = row.ConditionRecords[currentSpringIndex];

		if (currentSpringIndex == 0)
		{
			// Base cases for when we are considering the first spring at index 0
			return CalculateBasePossibleArrangements(currentGroupIndex, currentGroupSize, springCondition);
		}

		long possibleArrangementCount = 0;

		if (springCondition is SpringCondition.Operational or SpringCondition.Unknown)
		{
			possibleArrangementCount += CalculatePossibleArrangementsAssumingCurrentSpringOperational(currentSpringIndex, currentGroupIndex, currentGroupSize, row);
		}

		if (springCondition is SpringCondition.Damaged or SpringCondition.Unknown)
		{
			possibleArrangementCount += CalculatePossibleArrangementsAssumingCurrentSpringDamaged(currentSpringIndex, currentGroupIndex, currentGroupSize);
		}

		return possibleArrangementCount;
	}

	/// <summary>
	/// Calculates values for base cases of first spring at index 0, does not use memo as it is not needed.
	/// </summary>
	private static long CalculateBasePossibleArrangements(int currentGroupIndex, int currentGroupSize, SpringCondition firstSpringCondition)
	{
		// Base arrangements work on an assumption of i = 0 as base only applies to the first spring

		if (currentGroupIndex > 0)
		{
			// This is not possible as we can't consider group any other that first one if we are on a first character
			return 0;
		}

		// Based on the first spring condition and size of last group, we can have only either one possible arrangement (1)
		// or given arrangement is impossible (0)

		return firstSpringCondition switch
		{
			// The first spring being damaged is only possible if the last group size is exactly 1 (this spring), otherwise it is not possible
			SpringCondition.Damaged => currentGroupSize == 1 ? 1 : 0,
			// The first spring being operational is only possible if the last group size is exactly 0 (no damaged spring), otherwise it is not possible
			SpringCondition.Operational => currentGroupSize == 0 ? 1 : 0,
			// If the first spring is unknown, `k` can be either 0 or 1, since we don't know the state of the first spring, but can't have a group size > 1 as we have only considered single spring
			SpringCondition.Unknown => currentGroupSize < 2 ? 1 : 0,
			_ => throw new DaySolverException($"Invalid spring condition {firstSpringCondition}")
		};
	}

	/// <summary>
	/// Calculates number of possible arrangements assuming that the current spring (at currentSpringIndex) is operational.
	/// This can either be when the state of the spring is known to be operational, or it is unknown and *can* be operational.
	/// It may use memo from previous spring (at index currentSpringIndex - 1) to calculate the number of possible arrangements.
	/// </summary>
	private long CalculatePossibleArrangementsAssumingCurrentSpringOperational(int currentSpringIndex, int currentGroupIndex, int currentGroupSize, SpringRow row)
	{
		if (currentGroupSize > 0)
		{
			// This is not possible since we cannot have a current group size of more than 0 while considering the last spring as operational.
			return 0;
		}

		if (currentGroupIndex == 0)
		{
			// If we are considering first damage group with empty final group (currentGroupSize == 0 asserted by previous condition)
			// that means there should be one arrangement possible with no damaged springs before the current spring - otherwise that is not possible.
			bool containsDamagedSprings = ContainsDamaged(row.ConditionRecords.AsSpan()[..currentSpringIndex]);
			return containsDamagedSprings ? 0 : 1;
		}

		// Getting to this arrangement from previous spring (currentSpringIndex - 1) consists of two scenarios, either considering
		// previous damage group (currentGroupIndex - 1) ending with its full group size (row.DamagedGroupSizes[j - 1]) or considering
		// current damage group (currentGroupIndex) ending with empty group size (0)
		return _memo[currentSpringIndex - 1, currentGroupIndex - 1, row.DamagedGroupSizes[currentGroupIndex - 1]]
		     + _memo[currentSpringIndex - 1, currentGroupIndex, 0];
	}

	/// <summary>
	/// Calculates number of possible arrangements assuming that the current spring (at currentSpringIndex) is damaged.
	/// This can either be when the state of the spring is known to be damaged, or it is unknown and *can* be damaged.
	/// It may use memo from previous spring (at index currentSpringIndex - 1) to calculate the number of possible arrangements.
	/// </summary>
	private long CalculatePossibleArrangementsAssumingCurrentSpringDamaged(int currentSpringIndex, int currentGroupIndex, int currentGroupSize)
	{
		if (currentGroupSize == 0)
		{
			// This is not possible since we cannot have a current group size of 0 while considering the current spring as damaged.
			return 0;
		}

		// Number of ways to get to this arrangement is equal as the number of ways to get to the previous spring (currentSpringIndex - 1)
		// with the same damage group (currentGroupIndex), but one less size of the current group size (currentGroupIndex - 1)
		// as this step is just adding one more damaged spring to the group.
		return _memo[currentSpringIndex - 1, currentGroupIndex, currentGroupSize - 1];
	}

	/// <summary>
	/// Returns whether <paramref name="springSpan"/> contains any damaged springs.
	/// </summary>
	private static bool ContainsDamaged(ReadOnlySpan<SpringCondition> springSpan)
	{
		foreach (var item in springSpan)
		{
			if (item is SpringCondition.Damaged)
			{
				return true;
			}
		}

		return false;
	}
}
