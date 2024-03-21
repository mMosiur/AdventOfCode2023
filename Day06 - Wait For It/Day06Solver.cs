using AdventOfCode.Abstractions;
using AdventOfCode.Year2023.Day06.Puzzle;

namespace AdventOfCode.Year2023.Day06;

public sealed class Day06Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 6;
	public override string Title => "Wait For It";

	private readonly BoatRecordAnalyzer _recordAnalyzer;

	public Day06Solver(Day06SolverOptions options) : base(options)
	{
		_recordAnalyzer = new();
	}

	public Day06Solver(Action<Day06SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day06Solver() : this(new Day06SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		Part1InputReader part1InputReader = new();
		var records = part1InputReader.ReadRecords(Input);
		int result = records
			.Select(r => _recordAnalyzer.CountWaysToBeatRecord(r))
			.Aggregate((n1, n2) => n1 * n2);
		return result.ToString();
	}

	private int CountWaysToBeatTheRecord(BoatRecord record)
	{
		int count = 0;
		for (int timeHoldingButton = 1; timeHoldingButton < record.Time; timeHoldingButton++)
		{
			int timeLeft = record.Time - timeHoldingButton;
			int speed = timeHoldingButton;
			int distance = speed * timeLeft;
			if (distance > record.Distance)
			{
				count++;
			}
		}
		return count;
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
