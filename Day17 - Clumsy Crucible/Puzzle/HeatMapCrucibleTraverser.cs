namespace AdventOfCode.Year2023.Day17.Puzzle;

internal sealed class HeatMapCrucibleTraverser
{
	private readonly HeatLossMap _heatLossMap;
	private readonly Point _startingPoint;
	private readonly int _maxDistanceStraight;
	private readonly int[,,] _bestHeatLossBests;

	public HeatMapCrucibleTraverser(HeatLossMap heatLossMap, Point startingPoint, int maxDistanceStraight)
	{
		if (!heatLossMap.Bounds.Contains(startingPoint))
		{
			throw new ArgumentOutOfRangeException(nameof(startingPoint), startingPoint, "Starting point is outside the bounds of the heat loss map.");
		}

		_heatLossMap = heatLossMap;
		_startingPoint = startingPoint;
		_maxDistanceStraight = maxDistanceStraight;

		_bestHeatLossBests = new int[heatLossMap.Height, heatLossMap.Width, maxDistanceStraight];
		for (int r = 0; r < heatLossMap.Height; r++)
		{
			for (int c = 0; c < heatLossMap.Width; c++)
			{
				for (int d = 0; d < maxDistanceStraight; d++)
				{
					_bestHeatLossBests[r, c, d] = int.MaxValue;
				}
			}
		}

		_bestHeatLossBests[startingPoint.X, startingPoint.Y, 0] = 0;
	}

	public int Traverse(Point finishPoint)
	{
		if (!_heatLossMap.Bounds.Contains(finishPoint))
		{
			throw new ArgumentOutOfRangeException(nameof(finishPoint), finishPoint, "Finish point is outside the bounds of the heat loss map.");
		}

		PriorityQueue<TraverserState, int> queue = new();
		queue.Enqueue(new(_startingPoint, Direction.Right), 0);

		while (queue.TryDequeue(out TraverserState state, out int heatLossSum))
		{
			EnqueueIfValidAndBetter(queue, state.GoStraight(), heatLossSum);
			EnqueueIfValidAndBetter(queue, state.GoRight(), heatLossSum);
			EnqueueIfValidAndBetter(queue, state.GoLeft(), heatLossSum);
		}

		int smallestHeatLoss = int.MaxValue;
		for (int distance = 0; distance < _maxDistanceStraight; distance++)
		{
			smallestHeatLoss = Math.Min(_bestHeatLossBests[finishPoint.X, finishPoint.Y, distance], smallestHeatLoss);
		}

		return smallestHeatLoss;
	}

	private bool EnqueueIfValidAndBetter(PriorityQueue<TraverserState, int> queue, TraverserState state, int previousHeatLossSum)
	{
		if (!_heatLossMap.Bounds.Contains(state.Position)) return false;
		if (state.DistanceWithoutTurn > _maxDistanceStraight) return false;

		int heatLossSum = previousHeatLossSum + _heatLossMap[state.Position];
		int currentBestHeatLoss = _bestHeatLossBests[state.Position.X, state.Position.Y, state.DistanceWithoutTurn - 1];
		if (heatLossSum >= currentBestHeatLoss) return false;

		_bestHeatLossBests[state.Position.X, state.Position.Y, state.DistanceWithoutTurn - 1] = heatLossSum;
		queue.Enqueue(state, heatLossSum);
		return true;
	}
}

internal readonly record struct TraverserState(
	Point Position,
	Direction Direction,
	int DistanceWithoutTurn = 1)
{
	public TraverserState GoStraight()
	{
		var newPosition = Position.Move(Direction);
		return new(newPosition, Direction, DistanceWithoutTurn + 1);
	}

	public TraverserState GoRight()
	{
		var newDirection = Directions.TurnRight(Direction);
		var newPosition = Position.Move(newDirection);
		return new(newPosition, newDirection);
	}

	public TraverserState GoLeft()
	{
		var newDirection = Directions.TurnLeft(Direction);
		var newPosition = Position.Move(newDirection);
		return new(newPosition, newDirection);
	}
}

internal enum Direction
{
	Right = 0b00,
	Down = 0b01,
	Left = 0b10,
	Up = 0b11,
}

internal static class Directions
{
	public static Direction GoStraight(Direction direction) => (Direction)((int)direction & 0b11);

	public static Direction Opposite(Direction direction) => (Direction)(((int)direction ^ 0b10) & 0b11);

	public static Direction TurnRight(Direction direction) => (Direction)(((int)direction + 1) & 0b11);

	public static Direction TurnLeft(Direction direction) => (Direction)(((int)direction + 3) & 0b11);
}

internal static class PointExtensions
{
	public static Point Move(this Point point, Direction direction)
	{
		return direction switch
		{
			Direction.Right => new Point(point.X + 1, point.Y),
			Direction.Down => new Point(point.X, point.Y + 1),
			Direction.Left => new Point(point.X - 1, point.Y),
			Direction.Up => new Point(point.X, point.Y - 1),
			_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, "Invalid direction.")
		};
	}
}
