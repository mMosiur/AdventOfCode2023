using AdventOfCode.Year2023.Day07.Puzzle.Cards;

namespace AdventOfCode.Year2023.Day07.Puzzle.Hand;

internal sealed class Hand
{
	public IReadOnlyList<Card> Cards { get; }
	public HandType HandType { get; }
	public int Bid { get; }

	public Hand(string handRepresentation, HandType handType, int bid, bool treatJackAsJoker)
	{
		if (handRepresentation.Length is not 5)
		{
			throw new ArgumentException("Hand representation must contain exactly 5 characters", nameof(handRepresentation));
		}

		if (!Enum.IsDefined(handType))
		{
			throw new ArgumentException("Invalid hand type", nameof(handType));
		}

		Cards = handRepresentation.Select(c => new Card(c, treatJackAsJoker)).ToArray();
		HandType = handType;
		Bid = bid;
	}

	public override string ToString() => $"{string.Join(' ', Cards.Select(c => c.Label))} ({Bid})";
}
