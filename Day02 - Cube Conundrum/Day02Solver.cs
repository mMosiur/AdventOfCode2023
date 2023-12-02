using AdventOfCode.Abstractions;
using AdventOfCode.Year2023.Day02.Puzzle;

namespace AdventOfCode.Year2023.Day02;

public sealed class Day02Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 2;
	public override string Title => "Cube Conundrum";

	private readonly IReadOnlyList<Game> _games;
	private readonly GameValidator _gameValidator;

	public Day02Solver(Day02SolverOptions options) : base(options)
	{
		_games = InputLines
			.Select(InputReader.ParseGame)
			.ToList();
		_gameValidator = new(options.Part1RedCubeCount, options.Part1GreenCubeCount, options.Part1BlueCubeCount);
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
		int gameIdSum = _games.Where(g => _gameValidator.IsValid(g)).Sum(g => g.Id);
		return gameIdSum.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
