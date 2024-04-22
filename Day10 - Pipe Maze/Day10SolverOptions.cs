using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2023.Day10;

public sealed class Day10SolverOptions : DaySolverOptions
{
	public char VerticalTileChar { get; set; } = '|';
	public char HorizontalTileChar { get; set; } = '-';
	public char NorthEastTileChar { get; set; } = 'L';
	public char NorthWestTileChar { get; set; } = 'J';
	public char SouthWestTileChar { get; set; } = '7';
	public char SouthEastTileChar { get; set; } = 'F';
	public char GroundTileChar { get; set; } = '.';
	public char StartingPositionTileChar { get; set; } = 'S';
}
