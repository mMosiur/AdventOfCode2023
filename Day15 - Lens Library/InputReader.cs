using AdventOfCode.Common.SpanExtensions;

namespace AdventOfCode.Year2023.Day15;

internal static class InputReader
{
	public static List<string> ReadInitializationSequence(string input)
	{
		var sequence = new List<string>(input.AsSpan().Count(',') + 1);
		foreach (var sequenceSpan in input.AsSpan().Trim().Split(','))
		{
			sequence.Add(sequenceSpan.Trim().ToString());
		}

		return sequence;
	}
}
