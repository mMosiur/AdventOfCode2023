namespace AdventOfCode.Year2023.Day07.Puzzle;

internal sealed class HandBuilder
{
	private string? _handRepresentation;
	private int? _bid;

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

		return new(_handRepresentation, handType, _bid.Value);
	}

	public void Reset()
	{
		_handRepresentation = null;
		_bid = null;
	}

	private static HandType DetermineHandType(IEnumerable<char> cardRepresentations)
	{
		var groups = cardRepresentations.GroupBy(c => c).ToArray();
		switch (groups.Length)
		{
			case 0:
				throw new InvalidOperationException("No cards in hand");
			case 1:
				return HandType.FiveOfKind;
			case 2:
				// Variations of [1+4] or [3+2], it can be either FourOfKind or FullHouse
				int groupCount = groups[0].Count();
				return groupCount is 1 or 4
					? HandType.FourOfKind // if group count is 1 the other group must be 4
					: HandType.FullHouse; // one group count must be 3 and the other 5
			case 3:
				// Variations of [1+1+3] or [1+2+2], it can be either ThreeOfKind or TwoPair
				return groups.Any(g => g.Count() is 3)
					? HandType.ThreeOfKind // one group count must be 3 and the other two must be 1
					: HandType.TwoPair; // two group counts must be 2 and the other 1
			case 4:
				return HandType.OnePair; // one group count must be 2 and the other three must be 1
			case 5:
				return HandType.HighCard; // all group counts must be 1
			default:
				throw new InvalidOperationException("Invalid hand");
		}
	}
}
