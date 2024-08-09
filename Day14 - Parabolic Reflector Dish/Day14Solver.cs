using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2023.Day14;

public sealed class Day14Solver : DaySolver
{
	public override int Year => 2023;
	public override int Day => 14;
	public override string Title => "Parabolic Reflector Dish";

	private readonly SpaceType[,] _plane;

	public Day14Solver(Day14SolverOptions options) : base(options)
	{
		_plane = InputReader.Read(InputLines);
	}

	public Day14Solver(Action<Day14SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day14Solver() : this(new Day14SolverOptions())
	{
	}

	private RockFormation NewRockFormation()
	{
		return new((SpaceType[,])_plane.Clone());
	}

	public override string SolvePart1()
	{
		var rockFormation = NewRockFormation();
		rockFormation.TiltToSlideNorth();
		int totalLoad = rockFormation.CalculateTotalLoad();
		return totalLoad.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
