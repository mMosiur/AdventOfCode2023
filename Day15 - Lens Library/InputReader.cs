using AdventOfCode.Common.SpanExtensions;

namespace AdventOfCode.Year2023.Day15;

internal sealed class InputReader(Day15SolverOptions options)
{
    private readonly char _sequenceSeparator = options.SequenceSeparator;
    private readonly char _removeOperationChar = options.RemoveOperationChar;
    private readonly char _insertOperationChar = options.InsertOperationChar;


    public List<string> ReadInitializationSequence(string input)
    {
        var sequence = new List<string>(input.AsSpan().Count(_sequenceSeparator) + 1);
        foreach (var sequenceSpan in input.AsSpan().Trim().SplitAsSpans(_sequenceSeparator))
        {
            sequence.Add(sequenceSpan.Trim().ToString());
        }

        return sequence;
    }

    public InitializationStep ParseStep(string line) => ParseStep(line.AsSpan());

    public InitializationStep ParseStep(ReadOnlySpan<char> line)
    {
        line = line.Trim();
        int operationCharIndex = line.LastIndexOfAny(_removeOperationChar, '=');
        InitializationOperationType operation = ParseOperation(line[operationCharIndex]);
        var labelSpan = line[..operationCharIndex];
        string label = labelSpan.ToString();
        var parameterSpan = line[(operationCharIndex + 1)..];
        int parameter = parameterSpan.IsEmpty ? default : int.Parse(parameterSpan);
        return new(label, operation, parameter);
    }

    private InitializationOperationType ParseOperation(char operationChar)
    {
        if (operationChar == _removeOperationChar)
        {
            return InitializationOperationType.Remove;
        }

        if (operationChar == _insertOperationChar)
        {
            return InitializationOperationType.Insert;
        }

        throw new InvalidOperationException("Invalid operation character.");
    }
}
