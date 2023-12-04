using AdventOfCode.Abstractions;
using AdventOfCode.Year2023.Day03.Puzzle;
using AdventOfCode.Year2023.Day03.Puzzle.Schematic;

namespace AdventOfCode.Year2023.Day03;

public sealed class Day03Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 3;
	public override string Title => "Gear Ratios";

	private readonly Day03SolverOptions _options;
	private readonly InputReader _inputReader;
	private EngineSchematic? _engineSchematic;

	private EngineSchematic EngineSchematic => _engineSchematic ??= _inputReader.ReadInput();

	public Day03Solver(Day03SolverOptions options) : base(options)
	{
		_options = options;
		_inputReader = new(options.IgnoredSymbol, InputLines);
	}

	public Day03Solver(Action<Day03SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day03Solver() : this(new Day03SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		int sum = EngineSchematic.PartNumbers.Sum(pn => pn.Value);
		return sum.ToString();
	}

	public override string SolvePart2()
	{
		int gearRatioSum = EngineSchematic.Symbols
			.Where(s => s.Symbol == _options.GearSymbol)
			.Where(s => s.AdjacentNumberCount == _options.GearAdjacentPartNumberCount)
			.Sum(s => s.CalculateGearRatio());
		return gearRatioSum.ToString();
	}
}
