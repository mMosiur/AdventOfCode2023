using System.Text.RegularExpressions;
using AdventOfCode.Year2023.Day08.Map;

namespace AdventOfCode.Year2023.Day08.Input;

internal static class InputReader
{
	private static readonly Regex InputDataLineRegex = new(@"^\s*(\w+)\s*=\s*\((\w+)\s*,\s*(\w+)\)\s*$", RegexOptions.Compiled);

	public static MapDocuments Read(IEnumerable<string> inputLines)
	{
		try
		{
			using var enumerator = inputLines.GetEnumerator();
			if (!enumerator.MoveNext() || string.IsNullOrWhiteSpace(enumerator.Current))
			{
				throw new InputException("No input data");
			}

			var instructions = ReadInstructions(enumerator.Current);

			while (enumerator.MoveNext() && string.IsNullOrWhiteSpace(enumerator.Current))
			{
			}

			if (string.IsNullOrWhiteSpace(enumerator.Current))
			{
				throw new InputException("No nodes defined in input data");
			}

			var nodesBuilder = new NodesBuilder();
			do
			{
				var lineNodeData = ReadNodeDataLine(enumerator.Current);
				nodesBuilder.AddNode(lineNodeData.Label, lineNodeData.LeftLabel, lineNodeData.RightLabel);
			} while (enumerator.MoveNext() && !string.IsNullOrWhiteSpace(enumerator.Current));

			var nodes = nodesBuilder.Build(instructions);

			return new()
			{
				Instructions = instructions,
				Nodes = nodes,
			};
		}
		catch (InputException)
		{
			throw;
		}
		catch (Exception e)
		{
			throw new InputException("Invalid input data", e);
		}
	}

	private static List<Direction> ReadInstructions(ReadOnlySpan<char> instructionsLine)
	{
		instructionsLine = instructionsLine.Trim();
		var instructions = new List<Direction>(instructionsLine.Length);
		foreach (char c in instructionsLine)
		{
			instructions.Add(ParseDirection(c));
		}

		return instructions;
	}

	private static Direction ParseDirection(char c) => c switch
	{
		'L' => Direction.Left,
		'R' => Direction.Right,
		_ => throw new ArgumentOutOfRangeException(nameof(c), c, null),
	};

	private static InputNodeData ReadNodeDataLine(string nodeDataLine)
	{
		var match = InputDataLineRegex.Match(nodeDataLine);
		if (!match.Success)
		{
			throw new FormatException($"Invalid node data line: {nodeDataLine}");
		}

		return new()
		{
			Label = match.Groups[1].Value,
			LeftLabel = match.Groups[2].Value,
			RightLabel = match.Groups[3].Value
		};
	}
}
