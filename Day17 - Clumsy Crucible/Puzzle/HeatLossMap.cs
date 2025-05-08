namespace AdventOfCode.Year2023.Day17.Puzzle;

internal sealed class HeatLossMap
{
	private readonly int[,] _values;

	public int Height => _values.GetLength(0);
	public int Width => _values.GetLength(1);
	public Rectangle Bounds => new(0, Height - 1, 0, Width - 1);

	public HeatLossMap(int[,] heatLossMapValues)
	{
		ArgumentNullException.ThrowIfNull(heatLossMapValues);
		if (heatLossMapValues.GetLength(0) == 0 || heatLossMapValues.GetLength(1) == 0)
		{
			throw new ArgumentException("Heat loss map must have at least one row and one column.");
		}

		_values = (int[,])heatLossMapValues.Clone();
	}

	public int this[int x, int y] => _values[x, y];
	public int this[Point point] => _values[point.X, point.Y];
}
