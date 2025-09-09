namespace AdventOfCode.Year2023.Day18.Puzzle;

internal sealed class DigSite(DigPlan digPlan)
{
    private readonly DigPlan _digPlan = digPlan;

    public (HashSet<Point> Edges, HashSet<Point> Interior) DigOut(Point startingPoint)
    {
        var edges = DigOutEdges(startingPoint);
        var interiorStartingPoint = startingPoint + new Vector(1, 1);
        var interior = DigOutInterior(interiorStartingPoint, edges);
        return (edges, interior);
    }

    private HashSet<Point> DigOutEdges(Point startingPoint)
    {
        var point = startingPoint;
        HashSet<Point> edgePoints = [point];
        foreach (var row in _digPlan.Rows)
        {
            var stepVector = row.Direction.ToVector();
            for (int i = 0; i < row.Distance; i++)
            {
                point += stepVector;
                edgePoints.Add(point);
            }
        }

        return edgePoints;
    }

    private static HashSet<Point> DigOutInterior(Point startingPoint, HashSet<Point> edges)
    {
        // Flood fill from the starting point
        var interiorPoints = new HashSet<Point> { startingPoint };
        var pointsToCheck = new Stack<Point>();
        pointsToCheck.Push(startingPoint);

        ReadOnlySpan<Vector> directions =
        [
            Direction.Up.ToVector(),
            Direction.Left.ToVector(),
            Direction.Down.ToVector(),
            Direction.Right.ToVector(),
        ];
        while (pointsToCheck.Count > 0)
        {
            var point = pointsToCheck.Pop();
            foreach (var direction in directions)
            {
                var adjacentPoint = point + direction;
                if (edges.Contains(adjacentPoint)) continue;

                if (!interiorPoints.Add(adjacentPoint))
                {
                    continue;
                }

                pointsToCheck.Push(adjacentPoint);
            }
        }

        return interiorPoints;
    }
}
