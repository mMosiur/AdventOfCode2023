namespace AdventOfCode.Year2023.Day07.Puzzle;

internal enum HandType
{
	/// Hand type where all cards' labels are distinct
	HighCard = 1,
	/// Hand type where two cards share one label, and the other three cards have a different label from the pair and each other
	OnePair = 2,
	/// Hand type where two cards share one label, two other cards share a second label, and the remaining card has a third label
	TwoPair = 3,
	/// Hand type where three cards have the same label, and the remaining two cards are each different from any other card in the hand
	ThreeOfKind = 4,
	/// Hand type where three cards have the same label, and the remaining two cards share a different label
	FullHouse = 5,
	/// Hand type where four cards have the same label and one card has a different label
	FourOfKind = 6,
	/// Hand type where all five cards have the same label
	FiveOfKind = 7,
}
