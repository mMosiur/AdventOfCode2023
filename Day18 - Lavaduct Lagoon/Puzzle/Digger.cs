namespace AdventOfCode.Year2023.Day18.Puzzle;

internal sealed class Digger(IReadOnlyList<DigInstruction> digPlanInstructions)
{
    private readonly IReadOnlyList<DigInstruction> _digPlanInstructions = digPlanInstructions;

    public int CalculateDigSize(Point startingPoint)
    {
        var prevPoint = startingPoint;
        int edgeLength = 0;
        int sum = 0;
        foreach (var instruction in _digPlanInstructions)
        {
            edgeLength += instruction.Distance;
            var nextPoint = prevPoint + instruction.DigVector;
            sum += prevPoint.X * nextPoint.Y - nextPoint.X * prevPoint.Y;
            prevPoint = nextPoint;
        }

        if (prevPoint != startingPoint)
        {
            throw new InvalidOperationException("The dig plan does not return to the starting point.");
        }

        int digSize = Math.Abs(sum) / 2;
        int edgeSizeCorrection = edgeLength / 2 + 1;

        return digSize + edgeSizeCorrection;
    }
}
