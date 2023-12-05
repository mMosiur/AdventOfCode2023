using AdventOfCode.Abstractions;
using AdventOfCode.Year2023.Day05.Puzzle;

namespace AdventOfCode.Year2023.Day05;

public sealed class Day05Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 5;
	public override string Title => "If You Give A Seed A Fertilizer";

	private readonly InputReader _inputReader;

	public Day05Solver(Day05SolverOptions options) : base(options)
	{
		_inputReader = new InputReader();
	}

	public Day05Solver(Action<Day05SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day05Solver() : this(new Day05SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		var almanac = _inputReader.ReadInput(Input);
		uint[] numbers = almanac.SeedNumbers.ToArray();
		var currentNumberCategory = NumberCategory.Seed;
		while (currentNumberCategory is not NumberCategory.Location)
		{
			var numberMap = almanac[currentNumberCategory];
			for (int i = 0; i < numbers.Length; i++)
			{
				numbers[i] = numberMap.ConvertNumber(numbers[i]);
			}
			currentNumberCategory = numberMap.DestinationCategory;
		}
		return numbers.Min().ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
