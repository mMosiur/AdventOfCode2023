using AdventOfCode.Common;
using AdventOfCode.Common.Geometry;
using AdventOfCode.Year2023.Day11.Puzzle;

namespace AdventOfCode.Year2023.Day11;

public sealed class Day11Solver : DaySolver<Day11SolverOptions>
{
	public override int Year => 2023;
	public override int Day => 11;
	public override string Title => "Cosmic Expansion";

	private readonly IReadOnlyCollection<Point> _initialPositions;
	private readonly Day11SolverOptions _options;

	public Day11Solver(Day11SolverOptions options) : base(options)
	{
		_options = options;
		var inputReader = new InputReader(options.GalaxyChar);
		_initialPositions = inputReader.ReadGalaxyPositions(Input);
	}

	public Day11Solver(Action<Day11SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure)) { }

	public Day11Solver() : this(new Day11SolverOptions()) { }

	public override string SolvePart1()
	{
		var galaxyMap = new GalaxyMap(_initialPositions);
		galaxyMap.Expand(_options.PartOneExpansionMagnitude);
		long sum = SumDistancesBetweenGalaxies(galaxyMap);
		return sum.ToString();
	}

	public override string SolvePart2()
	{
		var galaxyMap = new GalaxyMap(_initialPositions);
		galaxyMap.Expand(_options.PartTwoExpansionMagnitude);
		long sum = SumDistancesBetweenGalaxies(galaxyMap);
		return sum.ToString();
	}

	private static long SumDistancesBetweenGalaxies(GalaxyMap galaxyMap)
	{
		long sum = 0;
		for (int i = 0; i < galaxyMap.Galaxies.Count; i++)
		{
			var g1 = galaxyMap.Galaxies[i];
			for (int j = i + 1; j < galaxyMap.Galaxies.Count; j++)
			{
				var g2 = galaxyMap.Galaxies[j];
				sum += MathG.ManhattanDistance(g1.Position, g2.Position);
			}
		}

		return sum;
	}
}
