namespace AdventOfCode.Year2023.Day15;

internal readonly struct InitializationStep(string label, InitializationOperationType operation, int parameter = default)
{
    public string Label { get; } = label;
    public InitializationOperationType Operation { get; } = operation;
    public int Parameter { get; } = parameter;
}
