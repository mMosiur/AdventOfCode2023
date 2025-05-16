namespace AdventOfCode.Year2023.Day13;

internal static class BitMath
{
    public static bool IsOneBitSet(int bits)
    {
        return bits != 0 && (bits & (bits - 1)) == 0;
    }

    public static bool IsAtMostOneBitSet(int bits)
    {
        return (bits & (bits - 1)) == 0;
    }
}
