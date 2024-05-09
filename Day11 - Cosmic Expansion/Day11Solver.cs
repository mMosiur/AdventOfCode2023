using AdventOfCode.Abstractions;
using AdventOfCode.Common.Geometry;
using AdventOfCode.Year2023.Day11.Puzzle;

namespace AdventOfCode.Year2023.Day11;

public sealed class Day11Solver : DaySolver
{
	private readonly GalaxyMap _galaxyMap;

	public Day11Solver(Day11SolverOptions options) : base(options)
	{
		var inputReader = new InputReader(options.GalaxyChar);
		var positions = inputReader.ReadGalaxyPositions(Input);
		_galaxyMap = new GalaxyMap(positions);
	}

	public Day11Solver(Action<Day11SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day11Solver() : this(new Day11SolverOptions())
	{
	}

	public override int Year => 2023;
	public override int Day => 11;
	public override string Title => "Cosmic Expansion";

	public override string SolvePart1()
	{
		_galaxyMap.Expand();
		int sum = 0;
		for (int i = 0; i < _galaxyMap.Galaxies.Count; i++)
		{
			var g1 = _galaxyMap.Galaxies[i];
			for (int j = i + 1; j < _galaxyMap.Galaxies.Count; j++)
			{
				var g2 = _galaxyMap.Galaxies[j];
				sum += MathG.ManhattanDistance(g1.Position, g2.Position);
			}
		}

		return sum.ToString();
	}

	public override string SolvePart2()
	{
		return "Unsolved";
	}
}
