using AdventOfCode.Year2023.Day10.Puzzle.Pipes;

namespace AdventOfCode.Year2023.Day10.Puzzle;

internal sealed class PipeMapAnalyzer(PipeMap pipeMap)
{
	private readonly PipeMap _pipeMap = pipeMap;

	public int GetLoopLength()
	{
		var traverser = _pipeMap.CreateTraverser();

		do
		{
			traverser.TakeStep();
		} while (traverser.CurrentPoint != _pipeMap.Start);

		return traverser.StepCount;
	}

	public int CalculateEnclosedArea()
	{
		// Shoelace formula for polygon area
		// https://en.wikipedia.org/wiki/Shoelace_formula
		var points = EnumerateLoopPoints().ToArray();

		int sum = 0;
		for (int i = 0; i < points.Length; i++)
		{
			int nextIndex = (i + 1) % points.Length;
			sum += GetPartialArea(points[i], points[nextIndex]);
		}

		int area = Math.Abs(sum / 2) + 1 - points.Length / 2;
		return area;
	}

	private static int GetPartialArea(Point a, Point b)
	{
		return a.X * b.Y - b.X * a.Y;
	}

	private IEnumerable<Point> EnumerateLoopPoints()
	{
		var traverser = _pipeMap.CreateTraverser();
		yield return traverser.CurrentPoint;
		traverser.TakeStep();

		while (traverser.CurrentPoint != _pipeMap.Start)
		{
			yield return traverser.CurrentPoint;
			traverser.TakeStep();
		}
	}
}
