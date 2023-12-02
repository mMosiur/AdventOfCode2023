namespace AdventOfCode.Year2023.Day02.Puzzle;

internal sealed class GameValidator(int redCubeCount, int greenCubeCount, int blueCubeCount)
{
	public bool IsValid(Game game)
		=> game.Cubes.All(IsCubeSetValid);

	private bool IsCubeSetValid(CubeSet cubeSet)
		=> cubeSet.Red <= redCubeCount && cubeSet.Green <= greenCubeCount && cubeSet.Blue <= blueCubeCount;
}
