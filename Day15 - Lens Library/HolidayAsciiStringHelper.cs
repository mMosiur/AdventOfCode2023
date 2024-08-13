namespace AdventOfCode.Year2023.Day15;

internal static class HolidayAsciiStringHelper
{
	/// <summary>
	/// The HASH algorithm is a way to turn any string of characters into a single number in the range 0 to 255.
	/// </summary>
	/// <remarks>
	/// To run the HASH algorithm on a string, start with a <b>current value</b> of <c>0</c>.
	/// Then, for each character in the string starting from the beginning:
	/// <list type="bullet">
	/// 	<item>Determine the ASCII code for the current character of the string.</item>
	/// 	<item>Increase the <b>current value</b> by the ASCII code you just determined.</item>
	/// 	<item>Set the <b>current value</b> to itself multiplied by <c>17</c>.</item>
	/// 	<item>Set the <b>current value</b> to the remainder of dividing itself by <c>256</c>.</item>
	/// </list>
	/// After following these steps for each character in the string in order, the current value is the output of the HASH algorithm.
	/// </remarks>
	/// <param name="input">The string of characters as input for the HASH algorithm.</param>
	/// <returns>A number between 0 and 255 as output of the HASH algorithm.</returns>
	public static int Hash(ReadOnlySpan<char> input)
	{
		int currentValue = 0;
		foreach (char c in input)
		{
			int asciiCode = (int)c;
			currentValue += asciiCode;
			currentValue *= 17;
			currentValue %= 256;
		}

		return currentValue;
	}

	/// <inheritdoc cref="Hash(ReadOnlySpan{char})"/>
	public static int Hash(string input) => Hash(input.AsSpan());
}
