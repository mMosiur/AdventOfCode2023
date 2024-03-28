namespace AdventOfCode.Year2023.Day07.Puzzle.Card;

internal readonly struct Card : IEquatable<Card>, IComparable<Card>
{
	private const string CardLabels = " J23456789TJQKA";

	public CardType Type { get; }

	public char Label => CardLabels[(int)Type];

	public Card(char label, bool treatJackAsJoker = false)
	{
		Type = label switch
		{
			'J' => treatJackAsJoker ? CardType.Joker : CardType.Jack,
			'2' => CardType.Two,
			'3' => CardType.Three,
			'4' => CardType.Four,
			'5' => CardType.Five,
			'6' => CardType.Six,
			'7' => CardType.Seven,
			'8' => CardType.Eight,
			'9' => CardType.Nine,
			'T' => CardType.Ten,
			'Q' => CardType.Queen,
			'K' => CardType.King,
			'A' => CardType.Ace,
			_ => throw new ArgumentException("Invalid card label", nameof(label)),
		};
	}

	private Card(Card card)
	{
		Type = card.Type;
	}

	private int NumericValue => (int)Type;

	#region Comparison

	public bool Equals(Card other)
	{
		return Type == other.Type;
	}

	public int CompareTo(Card other)
	{
		return NumericValue.CompareTo(other.NumericValue);
	}

	public override bool Equals(object? obj)
	{
		return obj is Card other && Equals(other);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(typeof(Card), Type);
	}

	public static bool operator ==(Card left, Card right)
	{
		return left.Equals(right);
	}

	public static bool operator !=(Card left, Card right)
	{
		return !left.Equals(right);
	}

	public static bool operator <(Card left, Card right)
	{
		return left.CompareTo(right) < 0;
	}

	public static bool operator >(Card left, Card right)
	{
		return left.CompareTo(right) > 0;
	}

	public static bool operator <=(Card left, Card right)
	{
		return left.CompareTo(right) <= 0;
	}

	public static bool operator >=(Card left, Card right)
	{
		return left.CompareTo(right) >= 0;
	}

	#endregion

	public override string ToString() => $"[{Type}]";
}
