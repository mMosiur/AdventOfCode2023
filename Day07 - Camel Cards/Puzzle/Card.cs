namespace AdventOfCode.Year2023.Day07.Puzzle;

internal readonly struct Card : IEquatable<Card>, IComparable<Card>
{
	public static readonly Dictionary<char, int> NumericValues = new()
	{
		{ '2', 2 },
		{ '3', 3 },
		{ '4', 4 },
		{ '5', 5 },
		{ '6', 6 },
		{ '7', 7 },
		{ '8', 8 },
		{ '9', 9 },
		{ 'T', 10 },
		{ 'J', 11 },
		{ 'Q', 12 },
		{ 'K', 13 },
		{ 'A', 14 },
	};

	public char Label { get; private init; }

	public Card(char label)
	{
		Label = label;
	}

	private Card(Card card)
	{
		Label = card.Label;
	}

	private int NumericValue => NumericValues[Label];

	#region Comparison

	public bool Equals(Card other)
	{
		return Label == other.Label;
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
		return HashCode.Combine(typeof(Card), Label);
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

	#region Available cards

	public static Card Two => new() { Label = '2' };
	public static Card Three => new() { Label = '3' };
	public static Card Four => new() { Label = '4' };
	public static Card Five => new() { Label = '5' };
	public static Card Six => new() { Label = '6' };
	public static Card Seven => new() { Label = '7' };
	public static Card Eight => new() { Label = '8' };
	public static Card Nine => new() { Label = '9' };
	public static Card Ten => new() { Label = 'T' };
	public static Card Jack => new() { Label = 'J' };
	public static Card Queen => new() { Label = 'Q' };
	public static Card King => new() { Label = 'K' };
	public static Card Ace => new() { Label = 'A' };

	#endregion

	public override string ToString() => $"[{Label}]";
}
