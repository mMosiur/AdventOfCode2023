namespace AdventOfCode.Year2023.Day18.Puzzle;

internal sealed class Digger(IReadOnlyList<DigInstruction> digPlanInstructions)
{
    private readonly IReadOnlyList<DigInstruction> _digPlanInstructions = digPlanInstructions;

    public long CalculateDigSize(Point startingPoint)
    {
        var prevPoint = startingPoint;
        long edgeLength = 0;
        long sum = 0;
        foreach (var instruction in _digPlanInstructions)
        {
            edgeLength += instruction.Distance;
            var nextPoint = prevPoint + instruction.DigVector;
            sum += (long)prevPoint.X * (long)nextPoint.Y - (long)nextPoint.X * (long)prevPoint.Y;
            prevPoint = nextPoint;
        }

        if (prevPoint != startingPoint)
        {
            throw new InvalidOperationException("The dig plan does not return to the starting point.");
        }

        long digSize = Math.Abs(sum) / 2;
        long edgeSizeCorrection = edgeLength / 2 + 1;

        return digSize + edgeSizeCorrection;
    }
}
