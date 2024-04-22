namespace AdventOfCode.Year2023.Day10.Puzzle.Pipes;

internal sealed class PipeMapTraverser
{
	private readonly PipeMap _pipeMap;
	private Direction? _cameFrom;

	public PipeMapTraverser(PipeMap pipeMap, Point startPoint)
	{
		if (!pipeMap.ContainsPoint(startPoint))
		{
			throw new ArgumentOutOfRangeException(nameof(startPoint), startPoint, "Start point is outside the bounds of the pipe map.");
		}

		_pipeMap = pipeMap;
		CurrentPoint = startPoint;
		StepCount = 0;
	}

	public Point CurrentPoint { get; private set; }
	public int StepCount { get; private set; }

	public PipeTile CurrentTile => _pipeMap[CurrentPoint];

	private bool CanGoUp =>
		CurrentTile.HasFlag(PipeTile.LeadsUp) &&
		_pipeMap.TryGetPoint(CurrentPoint + Step.Up, out var tileUp) &&
		tileUp.HasFlag(PipeTile.LeadsDown);

	private bool CanGoDown =>
		CurrentTile.HasFlag(PipeTile.LeadsDown) &&
		_pipeMap.TryGetPoint(CurrentPoint + Step.Down, out var tileDown) &&
		tileDown.HasFlag(PipeTile.LeadsUp);

	private bool CanGoLeft =>
		CurrentTile.HasFlag(PipeTile.LeadsLeft) &&
		_pipeMap.TryGetPoint(CurrentPoint + Step.Left, out var tileLeft) &&
		tileLeft.HasFlag(PipeTile.LeadsRight);

	private bool CanGoRight =>
		CurrentTile.HasFlag(PipeTile.LeadsRight) &&
		_pipeMap.TryGetPoint(CurrentPoint + Step.Right, out var tileRight) &&
		tileRight.HasFlag(PipeTile.LeadsLeft);

	public bool CanGo(Direction direction) => direction switch
	{
		Direction.Up => CanGoUp,
		Direction.Down => CanGoDown,
		Direction.Left => CanGoLeft,
		Direction.Right => CanGoRight,
		_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, "Invalid direction.")
	};

	public bool TryGo(Direction direction)
	{
		if (!CanGo(direction))
		{
			return false;
		}

		var step = Step.FromDirection(direction);
		CurrentPoint += step;
		_cameFrom = step.Direction.Opposite();
		StepCount++;
		return true;
	}

	public void TakeStep()
	{
		var rotations = Directions.EnumerateRotation(_cameFrom ?? Direction.Up, skipCurrent: _cameFrom.HasValue);
		foreach (var dir in rotations)
		{
			if (TryGo(dir))
			{
				return;
			}
		}

		throw new InvalidOperationException("No valid direction to go.");
	}
}
