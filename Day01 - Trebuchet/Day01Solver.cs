using AdventOfCode.Common;
using AdventOfCode.Year2023.Day01.Puzzle;

namespace AdventOfCode.Year2023.Day01;

public sealed class Day01Solver : DaySolver<Day01SolverOptions>
{
	public override int Year => 2023;
	public override int Day => 1;
	public override string Title => "Trebuchet?!";

	public Day01Solver(Day01SolverOptions options) : base(options) { }

	public Day01Solver(Action<Day01SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure)) { }

	public Day01Solver() : this(new Day01SolverOptions()) { }

	private int SumCalibrationValues(ICalibrationRetriever calibrationRetriever)
		=> InputLines
			.Select(l => calibrationRetriever.RetrieveCalibrationValue(l))
			.Sum();

	public override string SolvePart1()
	{
		var calibrationRetriever = new DigitCalibrationRetriever();
		int sum = SumCalibrationValues(calibrationRetriever);
		return sum.ToString();
	}

	public override string SolvePart2()
	{
		var calibrationRetriever = new SpellingDigitCalibrationRetriever();
		int sum = SumCalibrationValues(calibrationRetriever);
		return sum.ToString();
	}
}
