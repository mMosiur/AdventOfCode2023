using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2023.Day12;

public sealed class Day12Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 12;
	public override string Title => "Hot Springs";

	private readonly IReadOnlyList<SpringRow> _rows;

	public Day12Solver(Day12SolverOptions options) : base(options)
	{
		var inputReader = new InputReader();
		_rows = inputReader.ReadInput(InputLines);
	}

	public Day12Solver(Action<Day12SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day12Solver() : this(new Day12SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		var counter = new SpringArrangementCounter();
		var result = _rows.Select(r => counter.CountPossibleArrangements(r)).Sum();
		return result.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
