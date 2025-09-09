namespace AdventOfCode.Year2023.Day18.Puzzle;

internal readonly record struct Color(byte RedValue, byte GreenValue, byte BlueValue)
{
    public static Color FromHex(ReadOnlySpan<char> s, IFormatProvider? formatProvider = null)
    {
        if (s.StartsWith('#'))
        {
            s = s[1..];
        }

        if (s.Length != 6
         || !byte.TryParse(s[0..2], System.Globalization.NumberStyles.HexNumber, formatProvider, out byte redValue)
         || !byte.TryParse(s[2..4], System.Globalization.NumberStyles.HexNumber, formatProvider, out byte greenValue)
         || !byte.TryParse(s[4..6], System.Globalization.NumberStyles.HexNumber, formatProvider, out byte blueValue))
        {
            throw new ArgumentException($"Invalid color hex: '{s.ToString()}'");
        }

        return new(redValue, greenValue, blueValue);
    }
}
