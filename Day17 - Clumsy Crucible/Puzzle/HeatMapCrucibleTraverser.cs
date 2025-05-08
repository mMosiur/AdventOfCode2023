namespace AdventOfCode.Year2023.Day17.Puzzle;

internal sealed class HeatMapCrucibleTraverser
{
	private readonly HeatLossMap _heatLossMap;
	private readonly Point _startingPoint;
	private readonly int _maxDistanceStraight;
	private readonly int _minDistanceStraight;

	public HeatMapCrucibleTraverser(HeatLossMap heatLossMap, Point startingPoint, int maxDistanceStraight, int minDistanceStraight = 1)
	{
		if (!heatLossMap.Bounds.Contains(startingPoint))
		{
			throw new ArgumentOutOfRangeException(nameof(startingPoint), startingPoint, "Starting point is outside the bounds of the heat loss map.");
		}

		_heatLossMap = heatLossMap;
		_startingPoint = startingPoint;
		_maxDistanceStraight = maxDistanceStraight;
		_minDistanceStraight = minDistanceStraight;
	}

	public int Traverse(Point finishPoint)
	{
		PriorityQueue<TraverserState, int> queue = new();

		// Enqueue straight lines from all possible starting position directions
		ReadOnlySpan<TraverserState> startingStates =
		[
			new(_startingPoint, Directions.Right),
			new(_startingPoint, Directions.Down),
			new(_startingPoint, Directions.Left),
			new(_startingPoint, Directions.Up),
		];
		foreach (var startingState in startingStates)
		{
			EnqueueAllStraightFromState(0, startingState, queue);
		}

		HashSet<TraverserState> visitedStates = new();
		while (queue.TryDequeue(out var state, out int heatLossSum))
		{
			if (state.Position == finishPoint)
			{
				return heatLossSum;
			}

			if (!visitedStates.Add(state)) continue;

			ReadOnlySpan<TraverserState> turnStates = [state.TurnLeft(), state.TurnRight()];
			foreach (var turnState in turnStates)
			{
				EnqueueAllStraightFromState(heatLossSum, turnState, queue);
			}
		}

		throw new InvalidOperationException("No path found.");
	}

	private void EnqueueAllStraightFromState(int baseHeatLoss, TraverserState turnState, in PriorityQueue<TraverserState, int> queue)
	{
		int stepsTaken = 0;
		int newHeatLoss = baseHeatLoss;
		var stepState = turnState;
		while (stepsTaken < _maxDistanceStraight)
		{
			stepState = stepState.StepStraight();
			stepsTaken++;
			if (!_heatLossMap.Bounds.Contains(stepState.Position)) break;
			newHeatLoss += _heatLossMap[stepState.Position];
			if (stepsTaken < _minDistanceStraight) continue;
			queue.Enqueue(stepState, newHeatLoss);
		}
	}

	private readonly record struct TraverserState(Point Position, Vector Direction)
	{
		public TraverserState StepStraight() => this with { Position = Position + Direction };

		public TraverserState TurnRight() => this with { Direction = Directions.TurnRight(Direction) };

		public TraverserState TurnLeft() => this with { Direction = Directions.TurnLeft(Direction) };
	}
}
