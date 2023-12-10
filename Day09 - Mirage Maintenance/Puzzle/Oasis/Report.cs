namespace AdventOfCode.Year2023.Day09.Puzzle.Oasis;

internal sealed class Report(IEnumerable<ValueHistory> valueHistories)
{
	private readonly List<ValueHistory> _histories = valueHistories.ToList();

	public IReadOnlyList<ValueHistory> Histories => _histories;
}
