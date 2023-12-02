namespace AdventOfCode.Year2023.Day02.Puzzle;

internal sealed class SmallestSetCalculator
{
	public CubeSet CalculateSmallestCubeSet(Game game)
	{
		int maxRedCubes = game.Cubes.Max(cube => cube.Red);
		int maxGreenCubes = game.Cubes.Max(cube => cube.Green);
		int maxBlueCubes = game.Cubes.Max(cube => cube.Blue);
		return new CubeSet
		{
			Red = maxRedCubes,
			Green = maxGreenCubes,
			Blue = maxBlueCubes
		};
	}
}
