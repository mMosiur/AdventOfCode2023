using AdventOfCode.Abstractions;
using AdventOfCode.Year2023.Day03.Puzzle;
using AdventOfCode.Year2023.Day03.Puzzle.Schematic;

namespace AdventOfCode.Year2023.Day03;

public sealed class Day03Solver(Day03SolverOptions options) : DaySolver(options)
{
	public override int Year => 2023;
	public override int Day => 3;
	public override string Title => "Gear Ratios";

	private readonly InputReader _inputReader = new(options.IgnoredSymbol);
	private EngineSchematic? _engineSchematic;

	private EngineSchematic EngineSchematic => _engineSchematic ??= BuildEngineSchematic();

	public Day03Solver(Action<Day03SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day03Solver() : this(new Day03SolverOptions())
	{
	}

	private EngineSchematic BuildEngineSchematic() => _inputReader.ReadInput(InputLines);

	public override string SolvePart1()
	{
		int sum = 0;
		foreach (var number in EngineSchematic.Numbers)
		{
			if (number.Position.AdjacentPoints.Any(p => EngineSchematic.SymbolPositions.Contains(p)))
			{
				sum += number.Value;
			}
		}

		return sum.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
