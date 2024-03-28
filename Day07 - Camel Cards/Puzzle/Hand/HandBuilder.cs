using AdventOfCode.Year2023.Day07.Puzzle.Cards;

namespace AdventOfCode.Year2023.Day07.Puzzle.Hand;

internal sealed class HandBuilder
{
	private string? _handRepresentation;
	private int? _bid;
	private readonly bool _treatJackAsJoker;

	public HandBuilder(bool treatJackAsJoker)
	{
		_treatJackAsJoker = treatJackAsJoker;
	}

	public HandBuilder WithCards(string handRepresentation)
	{
		_handRepresentation = handRepresentation;
		return this;
	}

	public HandBuilder WithBid(int bid)
	{
		_bid = bid;
		return this;
	}

	public Hand Build()
	{
		if (_handRepresentation is null)
		{
			throw new InvalidOperationException("Hand representation is not set");
		}

		if (_bid is null)
		{
			throw new InvalidOperationException("Bid is not set");
		}

		var handType = DetermineHandType(_handRepresentation);

		return new(_handRepresentation, handType, _bid.Value, _treatJackAsJoker);
	}

	public void Reset()
	{
		_handRepresentation = null;
		_bid = null;
	}

	private HandType DetermineHandType(IEnumerable<char> cardRepresentations)
	{
		var groups = cardRepresentations.GroupBy(c => c).ToArray();
		return _treatJackAsJoker
			? DetermineHandTypeWithJokers(groups)
			: DetermineHandTypeWithoutJokers(groups);
	}


	private static HandType DetermineHandTypeWithoutJokers(IReadOnlyCollection<IGrouping<char, char>> groups)
	{
		return groups.Count switch
		{
			// All cards are the same with a single 5 card group
			1 => HandType.FiveOfKind,

			// Variations of [1+4] or [3+2], it can be either FourOfKind or FullHouse
			2 => groups.First().Count() is 1 or 4
				? HandType.FourOfKind // if group card count is 1 the other group must have 4 cards and vice versa
				: HandType.FullHouse, // the first group card count must be instead 3 and the other 2 or vice versa

			// Variations of [1+1+3] or [1+2+2], it can be either ThreeOfKind or TwoPair
			3 => groups.Any(g => g.Count() is 2)
				? HandType.TwoPair // if one group has 2 card, the other two groups must have 2 and 1 cards and so two pairs are present
				: HandType.ThreeOfKind, // if there is no 2 card group then there must be a 3 card group and two 1 card groups

			// A single group must contain 2 cards with three other groups containing 1 card each making one pair
			4 => HandType.OnePair,

			// All groups must be single card groups
			5 => HandType.HighCard,

			_ => throw new InvalidOperationException("Invalid hand")
		};
	}

	private static HandType DetermineHandTypeWithJokers(IReadOnlyCollection<IGrouping<char, char>> groups)
	{
		char jokerLabel = Card.GetCardTypeLabel(CardType.Joker);
		var jokers = groups.FirstOrDefault(g => g.Key == jokerLabel);

		if (jokers is null)
		{
			// There are no jokers so the hand can be determined without them
			return DetermineHandTypeWithoutJokers(groups);
		}

		if (groups.Count <= 2)
		{
			// Either all cards are jokers or there is a single card group aside from them
			// so they can be treated as that groups card type to make 5
			return HandType.FiveOfKind;
		}

		int jokerCount = jokers.Count();
		if (jokerCount is 4 or 5)
		{
			// There are no non-joker cards and all can be treated as the same card or there is one other card
			// and the jokers can be treated as that card
			return HandType.FiveOfKind;
		}

		if (groups.Count is 2)
		{
			// Aside from a joker there is only one other group and the jokers can be treated ad that cards group to make 5
			return HandType.FourOfKind;
		}

		if (groups.Count is 3)
		{
			if (jokerCount > 1)
			{
				// If there are more than one joker there must be either 2 or 3 of them.
				// If there are 3, pairing up with either of the other groups will make four of a kind.
				// If there are 2, exactly one other group will have 2 cards also and together they make four of a kind.
				return HandType.FourOfKind;
			}
			return groups.Any(g => g.Count() is 3)
				? HandType.FourOfKind // There is one group of 3 cards and one of 1 card, the single joker makes a four of a kind with the former
				: HandType.FullHouse; // There are two groups of 2 cards and one joker, adding to either pair makes a full house
		}

		if (groups.Count is 4)
		{
			// There are either three groups of 1 card and one group of 2 jokers where adding them to any group makes three of a kind,
			// or there is one group of 2 cards, two of 1 card and single joker, which added to the former group also makes three of a kind
			return HandType.ThreeOfKind;
		}

		// There are four groups of 1 card and single joker, adding it to any group makes a single pair
		return HandType.OnePair;
	}
}
