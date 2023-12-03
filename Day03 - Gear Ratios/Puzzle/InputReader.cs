using AdventOfCode.Common.EnumerableExtensions;
using AdventOfCode.Year2023.Day03.Puzzle.Schematic;

namespace AdventOfCode.Year2023.Day03.Puzzle;

internal sealed class InputReader(char ignoredSymbol)
{
	public EngineSchematic ReadInput(IEnumerable<string> inputLines)
	{
		HashSet<Point> symbolPositions = new();
		List<SchematicNumber> numbers = new();
		foreach ((string line, int rowIndex) in inputLines.WithIndex())
		{
			int numberStartIndex = -1;
			int startedNumber = 0;
			foreach ((char c, int columnIndex) in line.WithIndex())
			{
				if (char.IsAsciiDigit(c))
				{
					int digit = (int)char.GetNumericValue(c);
					startedNumber = startedNumber * 10 + digit;
					if (numberStartIndex == -1)
					{
						numberStartIndex = columnIndex;
					}

					continue;
				}

				if (numberStartIndex >= 0)
				{
					var position = new Line(rowIndex, numberStartIndex, columnIndex - 1);
					var number = new SchematicNumber(position, startedNumber);
					numbers.Add(number);
					numberStartIndex = -1;
					startedNumber = 0;
				}

				if (c == ignoredSymbol) continue;

				symbolPositions.Add(new(rowIndex, columnIndex));
			}

			if (numberStartIndex >= 0)
			{
				var position = new Line(rowIndex, numberStartIndex, line.Length - 1);
				var number = new SchematicNumber(position, startedNumber);
				numbers.Add(number);
			}
		}

		return new(symbolPositions, numbers);
	}
}
