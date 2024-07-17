using System.Text.RegularExpressions;
using AdventOfCode.Common.SpanExtensions;

namespace AdventOfCode.Year2023.Day12;

internal sealed class InputReader
{
	private readonly char _operationalSpringChar;
	private readonly char _damagedSpringChar;
	private readonly char _unknownSpringChar;

	private static readonly Regex SpringRowRegex = new(@"^\s*(.+)\s+([\d,]+)\s*$", RegexOptions.Compiled);

	public InputReader(Day12SolverOptions options)
	{
		_operationalSpringChar = options.OperationalSpringChar;
		_damagedSpringChar = options.DamagedSpringChar;
		_unknownSpringChar = options.UnknownSpringChar;
	}

	public IReadOnlyList<InputRow> ReadInput(IEnumerable<string> inputLines)
	{
		List<InputRow> rows = new(1000);
		foreach (string line in inputLines)
		{
			if (string.IsNullOrWhiteSpace(line))
			{
				continue;
			}

			var match = SpringRowRegex.Match(line);
			if (!match.Success)
			{
				throw new InputException($"Invalid input line ('{line}')");
			}

			var springsSpan = match.Groups[1].ValueSpan;
			var springs = ReadSprings(springsSpan);

			var springGroupSizesSpan = match.Groups[2].ValueSpan;
			var springGroupSizes = ReadSpringGroupSizes(springGroupSizesSpan);

			rows.Add(new InputRow(springs, springGroupSizes));
		}

		return rows;
	}

	private IReadOnlyList<SpringCondition> ReadSprings(ReadOnlySpan<char> springs)
	{
		springs = springs.Trim();
		var result = new SpringCondition[springs.Length];
		for (int i = 0; i < springs.Length; i++)
		{
			result[i] = ParseSpringType(springs[i]);
		}

		return result;
	}

	private SpringCondition ParseSpringType(char c)
	{
		if (c == _operationalSpringChar)
		{
			return SpringCondition.Operational;
		}

		if (c == _damagedSpringChar)
		{
			return SpringCondition.Damaged;
		}

		if (c == _unknownSpringChar)
		{
			return SpringCondition.Unknown;
		}

		throw new InputException($"Invalid spring type character ('{c}')");
	}

	private IReadOnlyList<int> ReadSpringGroupSizes(ReadOnlySpan<char> springGroupSizes)
	{
		int count = springGroupSizes.Count(',');
		var result = new List<int>(count + 1);
		foreach (var part in springGroupSizes.Split(','))
		{
			if (!int.TryParse(part, out int size))
			{
				throw new InputException($"Invalid spring group size ('{part}')");
			}
			result.Add(size);
		}

		return result;
	}
}

internal enum SpringCondition
{
	Unknown,
	Operational,
	Damaged,
}

internal class InputRow(IReadOnlyList<SpringCondition> springs, IReadOnlyList<int> springGroupSizes)
{
	public IReadOnlyList<SpringCondition> Springs { get; } = springs;
	public IReadOnlyList<int> SpringGroupSizes { get; } = springGroupSizes;
}
