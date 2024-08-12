namespace AdventOfCode.Year2023.Day15;

internal sealed class Hasher
{
	public int Hash(ReadOnlySpan<char> input)
	{
		int currentValue = 0;
		foreach (char c in input)
		{
			currentValue += (int)c;
			currentValue *= 17;
			currentValue %= 256;
		}

		return currentValue;
	}
}
