using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Year2023.Day03.Puzzle.Schematic;

namespace AdventOfCode.Year2023.Day03.Puzzle;

internal sealed class InputReader(char ignoredSymbol, IEnumerable<string> inputLines)
{
	private readonly Dictionary<Point, SchematicNumber> _numbers = new();
	private readonly Dictionary<Point, SchematicSymbol> _symbols = new();
	private readonly IReadOnlyList<string> _input = inputLines.ToArray();

	private bool IsSymbol(char c) => c != ignoredSymbol && !char.IsAsciiDigit(c);

	private bool TryReadNumberFrom(int x, int y, IReadOnlyList<string> input, [NotNullWhen(true)] out SchematicNumber? schematicNumber) => TryReadNumberFrom(new(x, y), input, out schematicNumber);

	private bool TryReadNumberFrom(Point point, IReadOnlyList<string> input, [NotNullWhen(true)] out SchematicNumber? schematicNumber)
	{
		schematicNumber = null;
		if (point.X < 0 || point.X >= input.Count) return false;
		string line = input[point.X];
		if (point.Y < 0 || point.Y >= line.Length) return false;
		if (!char.IsAsciiDigit(line[point.Y]))
		{
			return false;
		}

		int numberStartIndex = point.Y;
		while (numberStartIndex >= 0 && char.IsAsciiDigit(line[numberStartIndex]))
		{
			numberStartIndex--;
		}

		numberStartIndex++;
		var originPoint = point with { Y = numberStartIndex };
		if (_numbers.TryGetValue(originPoint, out schematicNumber))
		{
			return true;
		}

		int numberEndIndex = point.Y;
		while (numberEndIndex < line.Length && char.IsAsciiDigit(line[numberEndIndex]))
		{
			numberEndIndex++;
		}

		numberEndIndex--;

		int value = int.Parse(line.AsSpan(numberStartIndex, numberEndIndex - numberStartIndex + 1));
		var position = new Line(point.X, numberStartIndex, numberEndIndex);
		schematicNumber = new(position, value);
		_numbers.Add(originPoint, schematicNumber);
		return true;
	}

	public EngineSchematic ReadInput()
	{
		_numbers.Clear();
		_symbols.Clear();
		for (int rowIndex = 0; rowIndex < _input.Count; rowIndex++)
		{
			string line = _input[rowIndex];
			for (int columnIndex = 0; columnIndex < line.Length; columnIndex++)
			{
				char c = line[columnIndex];
				if (!IsSymbol(c)) continue;
				var symbol = new SchematicSymbol
				{
					Position = new(rowIndex, columnIndex),
					Symbol = c
				};

				if (rowIndex > 0)
				{
					for (int i = columnIndex - 1; i <= columnIndex + 1; i++)
					{
						if (TryReadNumberFrom(rowIndex - 1, i, _input, out var aboveNumber))
						{
							i = aboveNumber.PositionSpan.Columns.End;
							symbol.AdjacentNumbers.Add(aboveNumber);
						}
					}
				}

				if (TryReadNumberFrom(rowIndex, columnIndex - 1, _input, out var leftNumber))
				{
					symbol.AdjacentNumbers.Add(leftNumber);
				}

				if (TryReadNumberFrom(rowIndex, columnIndex + 1, _input, out var rightNumber))
				{
					symbol.AdjacentNumbers.Add(rightNumber);
				}

				if (rowIndex < _input.Count - 1)
				{
					for (int i = columnIndex - 1; i <= columnIndex + 1; i++)
					{
						if (TryReadNumberFrom(rowIndex + 1, i, _input, out var belowNumber))
						{
							i = belowNumber.PositionSpan.Columns.End;
							symbol.AdjacentNumbers.Add(belowNumber);
						}
					}
				}

				_symbols.Add(new(rowIndex, columnIndex), symbol);
			}
		}

		return new(_numbers.Values, _symbols.Values);
	}
}
