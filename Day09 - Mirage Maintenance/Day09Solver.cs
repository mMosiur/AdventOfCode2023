using AdventOfCode.Abstractions;
using AdventOfCode.Year2023.Day09.Puzzle.Oasis;

namespace AdventOfCode.Year2023.Day09;

public sealed class Day09Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 9;
	public override string Title => "Mirage Maintenance";

	private readonly ReportReader _reportReader;
	private Report? _report;

	public Day09Solver(Day09SolverOptions options) : base(options)
	{
		_reportReader = new();
	}

	public Day09Solver(Action<Day09SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day09Solver() : this(new Day09SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		_report ??= _reportReader.ReadReport(InputLines);

		int result = _report
			.Histories
			.Select(h => h.PredictNextValue())
			.Sum();

		return result.ToString();
	}

	public override string SolvePart2()
	{
		_report ??= _reportReader.ReadReport(InputLines);

		int result = _report
			.Histories
			.Select(h => h.ExtrapolatePreviousValue())
			.Sum();

		return result.ToString();
	}
}
