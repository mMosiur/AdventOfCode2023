namespace AdventOfCode.Year2023.Day19.Puzzle;

internal sealed class RatingSystemCombinations
{
    public required Range XRange { get; set; }
    public required Range MRange { get; set; }
    public required Range ARange { get; set; }
    public required Range SRange { get; set; }

    public long Count => (long)XRange.Count * MRange.Count * ARange.Count * SRange.Count;

    public Range GetCategoryRange(RatingCategory category) => category switch
    {
        RatingCategory.X => XRange,
        RatingCategory.M => MRange,
        RatingCategory.A => ARange,
        RatingCategory.S => SRange,
        _ => throw new ArgumentOutOfRangeException(nameof(category), category, "Invalid rating category."),
    };

    public void SetCategoryRange(RatingCategory category, Range range)
    {
        switch (category)
        {
            case RatingCategory.X:
                XRange = range;
                break;
            case RatingCategory.M:
                MRange = range;
                break;
            case RatingCategory.A:
                ARange = range;
                break;
            case RatingCategory.S:
                SRange = range;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(category), category, "Invalid rating category.");
        }
    }

    public RatingSystemCombinations WithCategoryRange(RatingCategory category, Range range)
    {
        var newCombinations = Clone();
        newCombinations.SetCategoryRange(category, range);
        return newCombinations;
    }

    public RatingSystemCombinations Clone() => new()
    {
        XRange = XRange,
        MRange = MRange,
        ARange = ARange,
        SRange = SRange,
    };

    public static RatingSystemCombinations FullRange(int minValue, int maxValue) =>new()
    {
        XRange = new(minValue, maxValue),
        MRange = new(minValue, maxValue),
        ARange = new(minValue, maxValue),
        SRange = new(minValue, maxValue),
    };
}
