using System.Text.RegularExpressions;
using AdventOfCode.Common;
using AdventOfCode.Common.SpanExtensions;

namespace AdventOfCode.Year2023.Day12;

internal sealed class InputReader
{
	private const char OperationalSpringChar = '.';
	private const char DamagedSpringChar = '#';
	private const char UnknownSpringChar = '?';

	private static readonly Regex SpringRowRegex = new(@"^\s*(.+)\s+([\d,]+)\s*$", RegexOptions.Compiled);

	public SpringRow[] ReadInput(IEnumerable<string> inputLines)
	{
		List<SpringRow> rows = new(1000);
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

			rows.Add(new SpringRow(springs, springGroupSizes));
		}

		return rows.ToArray();
	}

	private SpringCondition[] ReadSprings(ReadOnlySpan<char> springs)
	{
		springs = springs.Trim();
		var result = new SpringCondition[springs.Length];
		for (int i = 0; i < springs.Length; i++)
		{
			result[i] = ParseSpringType(springs[i]);
		}

		return result;
	}

	private static SpringCondition ParseSpringType(char c)
		=> c switch
		{
			OperationalSpringChar => SpringCondition.Operational,
			DamagedSpringChar => SpringCondition.Damaged,
			UnknownSpringChar => SpringCondition.Unknown,
			_ => throw new InputException($"Invalid spring type character ('{c}')")
		};

	private int[] ReadSpringGroupSizes(ReadOnlySpan<char> springGroupSizes)
	{
		int count = springGroupSizes.Count(',');
		var result = new List<int>(count + 1);
		foreach (var part in springGroupSizes.SplitAsSpans(','))
		{
			if (!int.TryParse(part, out int size))
			{
				throw new InputException($"Invalid spring group size ('{part}')");
			}

			result.Add(size);
		}

		return result.ToArray();
	}
}
