using AdventOfCode.Abstractions;
using AdventOfCode.Year2023.Day02.Puzzle;

namespace AdventOfCode.Year2023.Day02;

public sealed class Day02Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 2;
	public override string Title => "Cube Conundrum";

	private readonly Day02SolverOptions _options;
	private readonly IReadOnlyList<Game> _games;

	public Day02Solver(Day02SolverOptions options) : base(options)
	{
		_options = options;
		_games = InputLines
			.Select(InputReader.ParseGame)
			.ToList();
	}

	public Day02Solver(Action<Day02SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day02Solver() : this(new Day02SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		var gameValidator = new GameValidator(_options.Part1RedCubeCount, _options.Part1GreenCubeCount, _options.Part1BlueCubeCount);
		int gameIdSum = _games.Where(g => gameValidator.IsValid(g)).Sum(g => g.Id);
		return gameIdSum.ToString();
	}

	public override string SolvePart2()
	{
		var smallestSetCalculator = new SmallestSetCalculator();
		int sumOfSmallestSetPowers = _games
			.Select(g => smallestSetCalculator.CalculateSmallestCubeSet(g))
			.Sum(cs => cs.Power);
		return sumOfSmallestSetPowers.ToString();
	}
}
