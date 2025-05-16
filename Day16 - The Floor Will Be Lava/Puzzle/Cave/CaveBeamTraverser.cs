namespace AdventOfCode.Year2023.Day16.Puzzle.Cave;

internal sealed class CaveBeamTraverser(CaveTileGrid caveTileGrid)
{
    private readonly CaveTileGrid _caveTileGrid = caveTileGrid;

    public DirectionSet[,] TraverseWithBeam(Point startingPosition, Direction startingDirection)
    {
        if (!_caveTileGrid.IsInBounds(startingPosition))
        {
            throw new ArgumentException("Starting position is out of bounds of the cave.", nameof(startingPosition));
        }

        var beamHead = new BeamHead(startingPosition, startingDirection);
        var directionSets = new DirectionSet[_caveTileGrid.Height, _caveTileGrid.Width];
        var beamQueue = new Queue<BeamHead>(1);
        beamQueue.Enqueue(beamHead);

        while (beamQueue.TryDequeue(out BeamHead head))
        {
            if (!_caveTileGrid.IsInBounds(head.Position))
            {
                continue;
            }

            var directionSet = directionSets[head.Position.X, head.Position.Y];

            if (directionSet.Contains(head.Heading))
            {
                // Another beam has already passed through this point with the same direction.
                continue;
            }

            directionSets[head.Position.X, head.Position.Y] = directionSet.With(head.Heading);

            EnqueueMovedBeamHeads(beamQueue, head);
        }

        return directionSets;
    }

    private void EnqueueMovedBeamHeads(Queue<BeamHead> beamQueue, BeamHead head)
    {
        CaveTile headTile = _caveTileGrid[head.Position];
        switch (headTile)
        {
            case CaveTile.Empty:
                beamQueue.Enqueue(head.MoveAhead());
                break;
            case var _ when headTile.IsMirror():
                var newHeading = headTile.Reflect(head.Heading);
                beamQueue.Enqueue(head.TurnAndMove(newHeading));
                break;
            case var _ when headTile.IsSplitter():
                var (newHeading1, newHeading2) = headTile.Split(head.Heading);
                beamQueue.Enqueue(head.TurnAndMove(newHeading1));
                if (newHeading2.HasValue)
                {
                    beamQueue.Enqueue(head.TurnAndMove(newHeading2.Value));
                }

                break;
        }
    }

    private readonly record struct BeamHead(Point Position, Direction Heading)
    {
        public BeamHead Turn(Direction direction) => this with { Heading = direction };
        public BeamHead MoveAhead() => this with { Position = Position.Move(Heading) };
        public BeamHead TurnAndMove(Direction direction) => new(Position.Move(direction), direction);
    }
}
