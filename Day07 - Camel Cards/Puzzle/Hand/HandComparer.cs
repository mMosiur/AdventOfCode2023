namespace AdventOfCode.Year2023.Day07.Puzzle.Hand;

internal sealed class HandComparer : IComparer<Hand>
{
	public int Compare(Hand? x, Hand? y)
	{
		if (ReferenceEquals(x, y)) return 0;
		if (y is null) return 1;
		if (x is null) return -1;

		int comparison = FirstOrderingRule(x, y);

		if (comparison != 0)
		{
			return comparison;
		}

		comparison = SecondOrderingRule(x, y);

		return comparison;
	}

	/// <remarks>
	/// Every hand is exactly one type. They relative strength should be reflected by numeric values of <see cref="HandType"/> enum.
	/// </remarks>
	/// <seealso cref="HandType"/>
	private static int FirstOrderingRule(Hand x, Hand y)
	{
		return x.HandType.CompareTo(y.HandType);
	}

	/// <remarks>
	/// If two hands have the same type, a second ordering rule takes effect.
	/// Start by comparing the first card in each hand.
	/// If these cards are different, the hand with the stronger first card is considered stronger.
	/// If the first card in each hand have the same label, however, then move on to considering the second card in each hand.
	/// If they differ, the hand with the higher second card wins; otherwise, continue with the third card in each hand, then the fourth, then the fifth.
	/// </remarks>
	private static int SecondOrderingRule(Hand x, Hand y)
	{
		return x.Cards.Zip(y.Cards)
			.Select(c => c.First.CompareTo(c.Second))
			.FirstOrDefault(c => c != 0, 0);
	}
}
