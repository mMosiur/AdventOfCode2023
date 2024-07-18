namespace AdventOfCode.Year2023.Day12;

internal class SpringRow(SpringCondition[] conditionRecords, int[] damagedGroupSizes)
{
	public SpringCondition[] ConditionRecords { get; } = conditionRecords;
	public int[] DamagedGroupSizes { get; } = damagedGroupSizes;
}
