namespace AdventOfCode.Year2023.Day03.Puzzle.Schematic;

internal readonly struct Line(int row, int startColumn, int endColumn)
{
	public int Row { get; } = row;
	public Range Columns { get; } = new(startColumn, endColumn);

	public IEnumerable<Point> AdjacentPoints
	{
		get
		{
			yield return new(Row, Columns.Start - 1);
			int row = Row - 1;
			for (int col = Columns.Start - 1; col <= Columns.End + 1; col++)
			{
				yield return new(row, col);
			}
			yield return new(Row, Columns.End + 1);
			row = Row + 1;
			for (int col = Columns.End + 1; col >= Columns.Start - 1 ; col--)
			{
				yield return new(row, col);
			}
		}
	}
}
