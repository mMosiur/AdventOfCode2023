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

internal readonly struct InitializationStep(string label, InitializationOperationType operation, int parameter = default)
{
	public string Label { get; } = label;
	public InitializationOperationType Operation { get; } = operation;
	public int Parameter { get; } = parameter;

	public static InitializationStep Parse(string line) => Parse(line.AsSpan());

	public static InitializationStep Parse(ReadOnlySpan<char> line)
	{
		line = line.Trim();
		int operationCharIndex = line.LastIndexOfAny('-', '=');
		InitializationOperationType operation = line[operationCharIndex] switch
		{
			'-' => InitializationOperationType.Remove,
			'=' => InitializationOperationType.Insert,
			_ => throw new InvalidOperationException("Invalid operation character.")
		};
		var labelSpan = line[..operationCharIndex];
		string label = labelSpan.ToString();
		var parameterSpan = line[(operationCharIndex + 1)..];
		int parameter = parameterSpan.IsEmpty ? default : int.Parse(parameterSpan);
		return new(label, operation, parameter);
	}
}

internal enum InitializationOperationType
{
	None,
	Remove,
	Insert,
}
